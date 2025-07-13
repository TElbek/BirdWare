using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using System.Reflection;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext) : IFugleturAnalyseQuery
    {
        public List<TripAnalysisResult> Analyser(long fugleturId, AnalyseTyper analyseType)
        {
            if (!birdWareContext.Fugletur.Any(f => f.Id == fugleturId)) return [];

            var vTur = FindFugletur(fugleturId);
            var artListe = HentArtListe(fugleturId);
            var artIdList = artListe.Select(x => x.Id).ToList();

            var analyseMetode = FindAnalyseMetodeForAnalyseType(analyseType);
            var analyseResultatListe = UdfoerAnalyse(vTur, artIdList, analyseMetode);

            Parallel.ForEach(analyseResultatListe, analyseResultat =>
            {
                var art = artListe.FirstOrDefault(a => a.Id == analyseResultat.ArtId);

                analyseResultat.ArtNavn = art?.Navn ?? string.Empty;
                analyseResultat.Speciel = art?.Speciel ?? false;
                analyseResultat.SU = art?.SU ?? false;
            });

            return analyseResultatListe;
        }

        private List<Art> HentArtListe(long fugleturId)
        {
            return [.. (from o in birdWareContext.Observation
                    join a in birdWareContext.Art on o.ArtId equals a.Id
                    where o.FugleturId == fugleturId
                    select a)];
        }

        private VTur FindFugletur(long fugleturId)
        {
            return (from f in birdWareContext.Fugletur
                    join l in birdWareContext.Lokalitet on f.LokalitetId equals l.Id
                    where f.Id == fugleturId
                    select new VTur
                    {
                        Aarstal = f.Aarstal,
                        Maaned = f.Maaned,
                        Dato = f.Dato,
                        Id = f.Id,
                        LokalitetId = l.Id,
                        RegionId = l.RegionId,
                    }).First();
        }

        private List<TripAnalysisResult> UdfoerAnalyse(VTur vTur, List<long> artIdList, MethodInfo? method)
        {
            return method?.Invoke(this, [artIdList, vTur]) as List<TripAnalysisResult> ?? [];
        }

        private MethodInfo? FindAnalyseMetodeForAnalyseType(AnalyseTyper analyseType)
        {
            return GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                        .FirstOrDefault(m => m.GetCustomAttribute<AnalyseTypeAttribute>()?.AnalyseType == analyseType);
        }

        [AnalyseType(AnalyseTyper.FoersteObsIDatabasen)]
        private List<TripAnalysisResult> GetFoersteObsIDatabasen(List<long> artIdList, VTur vTur)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q && 
                                          s.FugleturId < vTur.Id))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIDatabasen,
                    ArtId = artId
                });

            return [.. result];
        }

        [AnalyseType(AnalyseTyper.FoersteObsIDK)]
        private List<TripAnalysisResult> GetFoersteObsIDK(List<long> artIdList, VTur vTur)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q && 
                                     s.FugleturId < vTur.Id && 
                                     s.Fugletur.Lokalitet.RegionId > 0))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIDK,
                    ArtId = artId
                });

            return [.. result];
        }

        [AnalyseType(AnalyseTyper.FoersteObsIRegion)]
        private List<TripAnalysisResult> GetFoersteObsIRegion(List<long> artIdList, VTur vTur)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < vTur.Id &&
                                     s.Fugletur.Lokalitet.RegionId == vTur.RegionId))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIRegion,
                    ArtId = artId
                });

            return [.. result];
        }

        [AnalyseType(AnalyseTyper.FoersteObsForLokalitet)]
        private List<TripAnalysisResult> GetFoersteObsForLokalitet(List<long> artIdList, VTur vTur)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < vTur.Id &&
                                     s.Fugletur.LokalitetId == vTur.LokalitetId))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsForLokalitet,
                    ArtId = artId
                });

            return [.. result];
        }

        [AnalyseType(AnalyseTyper.FoersteObsIAar)]
        private List<TripAnalysisResult> GetFoersteObsIAar(List<long> artIdList, VTur vTur)
        {
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(q => q.Aarstal == vTur.Aarstal);

            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < vTur.Id && 
                                     s.Fugletur.Lokalitet.RegionId > 0 &&
                                     withAarstal.Any(r => r.FugleturId == s.FugleturId)))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIAar,
                    ArtId = artId
                });

            return [.. result];
        }

        [AnalyseType(AnalyseTyper.FoersteObsIMaaned)]
        private List<TripAnalysisResult> GetFoersteObsIMaaned(List<long> artIdList, VTur vTur)
        {
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned()
                                                     .Where(q => q.Maaned == vTur.Maaned);

            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < vTur.Id &&
                                     s.Fugletur.Lokalitet.RegionId > 0 &&
                                     withMaaned.Any(r => r.FugleturId == s.FugleturId)))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIMaaned,
                    ArtId = artId
                });

            return [.. result];
        }
    }
}