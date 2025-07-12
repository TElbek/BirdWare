using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext, IFugleturQuery fugleturQuery) : IFugleturAnalyseQuery
    {
        public async Task<List<TripAnalysisResult>> Analyser(long fugleturId)
        {
            var vTur = fugleturQuery.GetFugletur(fugleturId);

            var artListe = (from o in birdWareContext.Observation
                            join a in birdWareContext.Art on o.ArtId equals a.Id
                            where o.FugleturId == fugleturId
                            select a).ToList();

            var artIdList = artListe.Select(x => x.Id).ToList();

            var FoersteObsIDatabasenTask = GetFoersteObsIDatabasen(artIdList, fugleturId);
            var FoersteObsIDKTask = GetFoersteObsIDK(artIdList, fugleturId);
            var FoersteObsIRegionTask = GetFoersteObsIRegion(artIdList, fugleturId, vTur.RegionId);
            var FoersteObsForLokalitetTask = GetFoersteObsForLokalitet(artIdList, fugleturId, vTur.LokalitetId);
            var FoersteObsIAarTask = GetFoersteObsIAar(artIdList, fugleturId, vTur.Aarstal);
            var FoersteObsIMaanedTask = GetFoersteObsIMaaned(artIdList, fugleturId, vTur.Maaned);

            await Task.WhenAll(
                FoersteObsIDatabasenTask,
                FoersteObsIDKTask,
                FoersteObsIRegionTask,
                FoersteObsForLokalitetTask,
                FoersteObsIAarTask,
                FoersteObsIMaanedTask
            );

            var FoersteObsIDatabasen = await FoersteObsIDatabasenTask;
            var FoersteObsIDK = await FoersteObsIDKTask;
            var FoersteObsIRegion = await FoersteObsIRegionTask;
            var FoersteObsForLokalitet = await FoersteObsForLokalitetTask;
            var FoersteObsIAar = await FoersteObsIAarTask;
            var FoersteObsIMaaned = await FoersteObsIMaanedTask;

            var list = new List<TripAnalysisResult>();
            list.AddRange(FoersteObsIDatabasen);
            list.AddRange(FoersteObsIDK);
            list.AddRange(FoersteObsIRegion);
            list.AddRange(FoersteObsForLokalitet);
            list.AddRange(FoersteObsIAar);
            list.AddRange(FoersteObsIMaaned);

            Parallel.ForEach(list, item =>
            {
                var art = artListe.FirstOrDefault(a => a.Id == item.ArtId);

                item.ArtNavn = art?.Navn ?? string.Empty;
                item.Speciel = art?.Speciel ?? false;
                item.SU = art?.SU ?? false;
            });

            return list;
        }

        private Task<List<TripAnalysisResult>> GetFoersteObsIDatabasen(List<long> artIdList, long fugleturId)
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

            return Task.FromResult(result.ToList());
        }

        private Task<List<TripAnalysisResult>> GetFoersteObsIDK(List<long> artIdList, long fugleturId)
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

            return Task.FromResult(result.ToList());
        }

        private Task<List<TripAnalysisResult>> GetFoersteObsIRegion(List<long> artIdList, long fugleturId, long regionId)
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

            return Task.FromResult(result.ToList());
        }

        private Task<List<TripAnalysisResult>> GetFoersteObsForLokalitet(List<long> artIdList, long fugleturId, long lokalitetId)
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

            return Task.FromResult(result.ToList());
        }

        private Task<List<TripAnalysisResult>> GetFoersteObsIAar(List<long> artIdList, long fugleturId, long aarstal)
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

            return Task.FromResult(result.ToList());
        }

        private Task<List<TripAnalysisResult>> GetFoersteObsIMaaned(List<long> artIdList, long fugleturId, long maaned)
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

            return Task.FromResult(result.ToList());
        }
    }
}