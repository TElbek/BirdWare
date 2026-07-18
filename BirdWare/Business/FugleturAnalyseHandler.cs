using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;

namespace BirdWare.Business
{
    public class FugleturAnalyseHandler(IFugleturAnalyseQuery analyseQuery) : IFugleturAnalyseHandler
    {
        public IEnumerable<TripAnalysisResult> Analyser(long fugleturId)
        {
            var vTur = analyseQuery.FindFugletur(fugleturId);
            var artListe = analyseQuery.HentArtListe(fugleturId);
            var vObsLookUp = analyseQuery.FindAnalyseData(new Fugletur {Id = fugleturId}, artListe.Select(s => s.Id));

            foreach (var art in artListe)
            {
                var obsListe = vObsLookUp[art.Id];

                if (FoersteObsIDatabasen(obsListe)) 
                    yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDatabasen));

                if (FoersteObsIRegion(vTur, obsListe)) 
                    yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForKommune));

                if (FoersteObsForLokalitet(vTur, obsListe)) 
                    yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForLokalitet));

                if (vTur.RegionId > 0)
                {
                    if (FoersteObsIDK(obsListe)) 
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIDK));

                    if (FoersteObsForKommune(vTur, obsListe)) 
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsForKommune));

                    if (FoersteObsIMaaned(vTur, obsListe)) 
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIMaaned));

                    if (FoersteObsIAar(vTur, obsListe)) 
                        yield return PopulateArtInfo(artListe, TripAnalysisResultFactory(art, AnalyseTyper.FoersteObsIAar));
                }
            }
        }

        private static TripAnalysisResult PopulateArtInfo(IEnumerable<Art> artListe, TripAnalysisResult analyseResultat)
        {
            var art = artListe.Single(a => a.Id == analyseResultat.ArtId);
            analyseResultat.ArtNavn = art.Navn ?? string.Empty;
            analyseResultat.Speciel = art.Speciel;
            analyseResultat.SU = art.SU;
            return analyseResultat;
        }

        private static bool FoersteObsIDatabasen(IEnumerable<FugleturAnalyseData> analyseDataArt) =>
            !analyseDataArt.Any();

        private static bool FoersteObsIDK(IEnumerable<FugleturAnalyseData> analyseDataArt) =>
            !analyseDataArt.Any(q => q.RegionId > 0);

        private static bool FoersteObsIRegion(VTur vTur, IEnumerable<FugleturAnalyseData> analyseDataArt) =>
            !analyseDataArt.Any(q => q.RegionId == vTur.RegionId);

        private static bool FoersteObsForKommune(VTur vTur, IEnumerable<FugleturAnalyseData> analyseDataArt) =>
            !analyseDataArt.Any(q => q.KommuneId == vTur.KommuneId && vTur.KommuneId > 0);

        private static bool FoersteObsForLokalitet(VTur vTur, IEnumerable<FugleturAnalyseData> analyseDataArt) =>
            !analyseDataArt.Any(q => q.LokalitetId == vTur.LokalitetId);

        private static bool FoersteObsIAar(VTur vTur, IEnumerable<FugleturAnalyseData> analyseDataArt) =>
            !analyseDataArt.Any(q => q.Aarstal == vTur.Aarstal);

        private static bool FoersteObsIMaaned(VTur vTur, IEnumerable<FugleturAnalyseData> analyseDataArt) =>
            !analyseDataArt.Any(q => q.Maaned == vTur.Maaned);

        private static TripAnalysisResult TripAnalysisResultFactory(Art art, AnalyseTyper analyseType) =>
            new() { AnalyseType = analyseType, ArtId = art.Id };
    }
}