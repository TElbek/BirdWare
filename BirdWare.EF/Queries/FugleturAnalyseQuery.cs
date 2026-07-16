using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext) : IFugleturAnalyseQuery
    {
        public async Task<IEnumerable<TripAnalysisResult>> Analyser(long fugleturId)
        {
            var vTur = await FindFugletur(fugleturId);
            var artListe = await HentArtListe(fugleturId);
            var vObsLookUp = await FindAnalyseData(vTur, artListe);

            var tripAnalysisResult = new List<TripAnalysisResult>();

            tripAnalysisResult.AddRange(artListe
                .Where(a => FoersteObsIDatabasen(vObsLookUp[a.Id]).Result)
                .Select(a => PopulateArtInfo(artListe, TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIDatabasen))));

            tripAnalysisResult.AddRange(artListe
                .Where(a => FoersteObsIRegion(vTur, vObsLookUp[a.Id]).Result)
                .Select(a => PopulateArtInfo(artListe, TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIRegion))));

            tripAnalysisResult.AddRange(artListe
                .Where(a => FoersteObsForLokalitet(vTur, vObsLookUp[a.Id]).Result)
                .Select(a => PopulateArtInfo(artListe, TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsForLokalitet))));

            if(vTur.RegionId > 0)
            {
                tripAnalysisResult.AddRange(artListe
                    .Where(a => FoersteObsIDK(vObsLookUp[a.Id]).Result)
                    .Select(a => PopulateArtInfo(artListe, TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIDK))));

                tripAnalysisResult.AddRange(artListe
                    .Where(a => FoersteObsForKommune(vTur, vObsLookUp[a.Id]).Result)
                    .Select(a => PopulateArtInfo(artListe, TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsForKommune))));

                tripAnalysisResult.AddRange(artListe
                    .Where(a => FoersteObsIAar(vTur, vObsLookUp[a.Id]).Result)
                    .Select(a => PopulateArtInfo(artListe, TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIAar))));

                tripAnalysisResult.AddRange(artListe
                    .Where(a => FoersteObsIMaaned(vTur, vObsLookUp[a.Id]).Result)
                    .Select(a => PopulateArtInfo(artListe, TripAnalysisResultFactory(a, AnalyseTyper.FoersteObsIMaaned))));
            }

            return tripAnalysisResult;
        }

        private static TripAnalysisResult PopulateArtInfo(IEnumerable<Art> artListe, TripAnalysisResult analyseResultat)
        {
            var art = artListe.FirstOrDefault(a => a.Id == analyseResultat.ArtId);
            analyseResultat.ArtNavn = art?.Navn ?? string.Empty;
            analyseResultat.Speciel = art?.Speciel ?? false;
            analyseResultat.SU = art?.SU ?? false;
            return analyseResultat;
        }

        private async Task<IEnumerable<Art>> HentArtListe(long fugleturId)
        {
            return await birdWareContext.Observation.AsNoTracking()
                        .Where(o => o.FugleturId == fugleturId)
                        .Select(o => o.Art)
                        .Distinct()
                        .ToListAsync();
        }

        private async Task<VTur> FindFugletur(long fugleturId)
        {
            if (birdWareContext.Fugletur.Any(q => q.Id == fugleturId))
            {
                return birdWareContext.Fugletur.AsNoTracking()
                    .Where(f => f.Id == fugleturId)
                    .Select(f => new VTur
                    {
                        Aarstal = f.Aarstal,
                        Maaned = f.Maaned,
                        Dato = f.Dato,
                        Id = f.Id,
                        LokalitetId = f.LokalitetId,
                        KommuneId = f.Lokalitet.KommuneId,
                        RegionId = f.Lokalitet.RegionId,
                    }).First();
            }
            return new VTur();
        }

        private async Task<ILookup<long, VObs>> FindAnalyseData(VTur vTur, IEnumerable<Art> artList)
        {
            return birdWareContext.Observation.AsNoTracking()
                .Where(o => o.FugleturId < vTur.Id && artList.Select(s => s.Id).Contains(o.ArtId))
                .Select(o => new VObs
                {
                    ArtId = o.ArtId,
                    LokalitetId = o.Fugletur.LokalitetId,
                    KommuneId = o.Fugletur.Lokalitet.KommuneId,
                    RegionId = o.Fugletur.Lokalitet.RegionId,
                    Aarstal = o.Fugletur.Aarstal,
                    Maaned = o.Fugletur.Maaned,
                }).Distinct().ToLookup(l => l.ArtId);
        }

        private static async Task<bool> FoersteObsIDatabasen(IEnumerable<VObs> analyseDataArt)
        {
            return !await Task.FromResult(analyseDataArt.Any());
        }

        private static async Task<bool> FoersteObsIDK(IEnumerable<VObs> analyseDataArt)
        {
            return !await Task.FromResult(analyseDataArt.Any(q => q.RegionId > 0));
        }

        private static async Task<bool> FoersteObsIRegion(VTur vTur, IEnumerable<VObs> analyseDataArt)
        {
            return !await Task.FromResult(analyseDataArt.Any(q => q.RegionId == vTur.RegionId));
        }

        private static async Task<bool> FoersteObsForKommune(VTur vTur, IEnumerable<VObs> analyseDataArt)
        {
            return !await Task.FromResult(analyseDataArt.Any(q => q.KommuneId == vTur.KommuneId && vTur.KommuneId > 0));
        }

        private static async Task<bool> FoersteObsForLokalitet(VTur vTur, IEnumerable<VObs> analyseDataArt)
        {
            return !await Task.FromResult(analyseDataArt.Any(q => q.LokalitetId == vTur.LokalitetId));
        }

        private static async Task<bool> FoersteObsIAar(VTur vTur, IEnumerable<VObs> analyseDataArt)
        {
            return !await Task.FromResult(analyseDataArt.Any(q => q.Aarstal == vTur.Aarstal && q.RegionId > 0));
        }

        private static async Task<bool> FoersteObsIMaaned(VTur vTur, IEnumerable<VObs> analyseDataArt)
        {
            return !await Task.FromResult(analyseDataArt.Any(q => q.Maaned == vTur.Maaned && q.RegionId > 0));
        }

        private static TripAnalysisResult TripAnalysisResultFactory(Art art, AnalyseTyper analyseType)
        {
            return new TripAnalysisResult { AnalyseType = analyseType, ArtId = art.Id };
        }
    }
}