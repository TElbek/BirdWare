using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuery(BirdWareContext birdWareContext) : IFugleturAnalyseQuery
    {
        public IEnumerable<TripAnalysisResult> Analyser(long fugleturId)
        {
            var vTur = FindFugletur(fugleturId);
            var artListe = HentArtListe(fugleturId);
            var vObsLookUp = FindAnalyseData(vTur, artListe);

            foreach (var art in artListe)
            {
                var obsListe = vObsLookUp[art.Id];

                if(FoersteObsIDatabasen(obsListe)) yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDatabasen));
                if(FoersteObsIRegion(vTur, obsListe)) yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForKommune));
                if(FoersteObsForLokalitet(vTur, obsListe)) yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForLokalitet));

                if (vTur.RegionId > 0)
                {
                    if (FoersteObsIDK(obsListe)) yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDK));
                    if (FoersteObsForKommune(vTur, obsListe)) yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForKommune));
                    if (FoersteObsIMaaned(vTur, obsListe)) yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIMaaned));
                    if (FoersteObsIAar(vTur, obsListe)) yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIAar));
                }
            }           
        }

        private static TripAnalysisResult PopulateArtInfo(IEnumerable<Art> artListe, TripAnalysisResult analyseResultat)
        {
            var art = artListe.FirstOrDefault(a => a.Id == analyseResultat.ArtId);
            analyseResultat.ArtNavn = art?.Navn ?? string.Empty;
            analyseResultat.Speciel = art?.Speciel ?? false;
            analyseResultat.SU = art?.SU ?? false;
            return analyseResultat;
        }

        private List<Art> HentArtListe(long fugleturId)
        {
            return [.. birdWareContext.Observation.AsNoTracking()
                   .Where(o => o.FugleturId == fugleturId)
                   .Select(o => new Art { Id = o.ArtId, Navn = o.Art.Navn, Speciel = o.Art.Speciel, SU = o.Art.SU })];
        }

        private VTur FindFugletur(long fugleturId)
        {
            if (!birdWareContext.Fugletur.Any(q => q.Id == fugleturId)) return new VTur(); 
            
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

        private ILookup<long, VObs> FindAnalyseData(VTur vTur, IEnumerable<Art> artList)
        {
            return birdWareContext.Observation.AsNoTracking()
                .Where(o => o.FugleturId < vTur.Id && artList.Select(s => s.Id).Contains(o.ArtId))
                .Select(o => new VObs
                { ArtId = o.ArtId,
                  LokalitetId = o.Fugletur.LokalitetId,
                  KommuneId = o.Fugletur.Lokalitet.KommuneId,
                  RegionId = o.Fugletur.Lokalitet.RegionId,
                  Aarstal = o.Fugletur.Aarstal,
                  Maaned = o.Fugletur.Maaned})
                . Distinct()
                .ToLookup(l => l.ArtId);
        }

        private static bool FoersteObsIDatabasen(IEnumerable<VObs> analyseDataArt) => 
            !analyseDataArt.Any();

        private static bool FoersteObsIDK(IEnumerable<VObs> analyseDataArt) => 
            !analyseDataArt.Any(q => q.RegionId > 0);

        private static bool FoersteObsIRegion(VTur vTur, IEnumerable<VObs> analyseDataArt) => 
            !analyseDataArt.Any(q => q.RegionId == vTur.RegionId);

        private static bool FoersteObsForKommune(VTur vTur, IEnumerable<VObs> analyseDataArt) => 
            !analyseDataArt.Any(q => q.KommuneId == vTur.KommuneId && vTur.KommuneId > 0);

        private static bool FoersteObsForLokalitet(VTur vTur, IEnumerable<VObs> analyseDataArt) => 
            !analyseDataArt.Any(q => q.LokalitetId == vTur.LokalitetId);

        private static bool FoersteObsIAar(VTur vTur, IEnumerable<VObs> analyseDataArt) => 
            !analyseDataArt.Any(q => q.Aarstal == vTur.Aarstal && q.RegionId > 0);

        private static bool FoersteObsIMaaned(VTur vTur, IEnumerable<VObs> analyseDataArt) => 
            !analyseDataArt.Any(q => q.Maaned == vTur.Maaned && q.RegionId > 0);

        private static TripAnalysisResult TripAnalysisResultFactory(Art art, AnalyseTyper analyseType) => 
            new() { AnalyseType = analyseType, ArtId = art.Id };
    }
}