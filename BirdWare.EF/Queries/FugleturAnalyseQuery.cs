using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext, IFugleturQuery fugleturQuery) : IFugleturAnalyseQuery
    {
        public List<TripAnalysisResult> Analyser(long fugleturId, AnalyseTyper analyseType)
        {
            var vTur = fugleturQuery.GetFugletur(fugleturId);

            var artListe = (from o in birdWareContext.Observation
                            join a in birdWareContext.Art on o.ArtId equals a.Id
                            where o.FugleturId == fugleturId
                            select a).ToList();

            var artIdList = artListe.Select(x => x.Id).ToList();
            var list = new List<TripAnalysisResult>();

            switch (analyseType)
            { 
                case AnalyseTyper.FoersteObsIDatabasen:
                    list = GetFoersteObsIDatabasen(artIdList, fugleturId);
                    break;
                case AnalyseTyper.FoersteObsIDK:
                    list = GetFoersteObsIDK(artIdList, fugleturId);
                    break;
                case AnalyseTyper.FoersteObsIRegion:
                    list = GetFoersteObsIRegion(artIdList, fugleturId, vTur.RegionId);
                    break;
                case AnalyseTyper.FoersteObsForLokalitet:
                    list = GetFoersteObsForLokalitet(artIdList, fugleturId, vTur.LokalitetId);
                    break;
                case AnalyseTyper.FoersteObsIAar:
                    list = GetFoersteObsIAar(artIdList, fugleturId, vTur.Aarstal);
                    break;
                case AnalyseTyper.FoersteObsIMaaned:
                    list = GetFoersteObsIMaaned(artIdList, fugleturId, vTur.Maaned);
                    break;
            }

            Parallel.ForEach(list, item =>
            {
                var art = artListe.FirstOrDefault(a => a.Id == item.ArtId);

                item.ArtNavn = art?.Navn ?? string.Empty;
                item.Speciel = art?.Speciel ?? false;
                item.SU = art?.SU ?? false;
            });

            return list;
        }

        private List<TripAnalysisResult> GetFoersteObsIDatabasen(List<long> artIdList, long fugleturId)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q && 
                                          s.FugleturId < fugleturId))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIDatabasen,
                    ArtId = artId
                });

            return [.. result];
        }

        private List<TripAnalysisResult> GetFoersteObsIDK(List<long> artIdList, long fugleturId)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q && 
                                     s.FugleturId < fugleturId && 
                                     s.Fugletur.Lokalitet.RegionId > 0))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIDK,
                    ArtId = artId
                });

            return [.. result];
        }

        private List<TripAnalysisResult> GetFoersteObsIRegion(List<long> artIdList, long fugleturId, long regionId)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < fugleturId &&
                                     s.Fugletur.Lokalitet.RegionId == regionId))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIRegion,
                    ArtId = artId
                });

            return [.. result];
        }

        private List<TripAnalysisResult> GetFoersteObsForLokalitet(List<long> artIdList, long fugleturId, long lokalitetId)
        {
            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < fugleturId &&
                                     s.Fugletur.LokalitetId == lokalitetId))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsForLokalitet,
                    ArtId = artId
                });

            return [.. result];
        }

        private List<TripAnalysisResult> GetFoersteObsIAar(List<long> artIdList, long fugleturId, long aarstal)
        {
            var withAarstal = birdWareContext.Fugletur.GetAarMaaned().Where(q => q.Aarstal == aarstal);

            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < fugleturId && 
                                     s.Fugletur.Lokalitet.RegionId > 0 &&
                                     withAarstal.Any(r => r.FugleturId == s.FugleturId)))
                .Select(artId => new TripAnalysisResult
                {
                    AnalyseType = AnalyseTyper.FoersteObsIAar,
                    ArtId = artId
                });

            return [.. result];
        }

        private List<TripAnalysisResult> GetFoersteObsIMaaned(List<long> artIdList, long fugleturId, long maaned)
        {
            var withMaaned = birdWareContext.Fugletur.GetAarMaaned()
                                                     .Where(q => q.Maaned == maaned);

            var result = artIdList
                .Where(q => !birdWareContext.Observation
                                .Any(s => s.ArtId == q &&
                                     s.FugleturId < fugleturId &&
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