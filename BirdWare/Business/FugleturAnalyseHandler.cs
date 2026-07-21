using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;

namespace BirdWare.Business
{
    public class FugleturAnalyseHandler(IFugleturAnalyseQuery analyseQuery) : IFugleturAnalyseHandler
    {
        public IEnumerable<TripAnalysisResult> Analyser(long fugleturId, AnalyseTyper analyseType)
        {
            var vTur = analyseQuery.FindFugletur(fugleturId);
            var artListe = analyseQuery.HentArtListe(fugleturId);
            var resultList = new List<TripAnalysisResult>();

            Parallel.ForEach(artListe, art =>
            {
                if (analyseType == AnalyseTyper.FoersteObsIDatabasen && FoersteObsIDatabasen(fugleturId, art.Id))
                    resultList.Add(PopulateArtInfo(artListe, TripAnalysisResultFactory(art, analyseType)));

                if (analyseType == AnalyseTyper.FoersteObsIRegion && FoersteObsIRegion(vTur, art.Id))
                    resultList.Add(PopulateArtInfo(artListe, TripAnalysisResultFactory(art, analyseType)));

                if (analyseType == AnalyseTyper.FoersteObsForLokalitet && FoersteObsForLokalitet(vTur, art.Id))
                    resultList.Add(PopulateArtInfo(artListe, TripAnalysisResultFactory(art, analyseType)));

                if (vTur.RegionId > 0)
                {
                    if (analyseType == AnalyseTyper.FoersteObsIDK && FoersteObsIDK(fugleturId, art.Id))
                        resultList.Add(PopulateArtInfo(artListe, TripAnalysisResultFactory(art, analyseType)));

                    if (analyseType == AnalyseTyper.FoersteObsForKommune && FoersteObsForKommune(vTur, art.Id))
                        resultList.Add(PopulateArtInfo(artListe, TripAnalysisResultFactory(art, analyseType)));

                    if (analyseType == AnalyseTyper.FoersteObsIMaaned && FoersteObsIMaaned(vTur, art.Id))
                        resultList.Add(PopulateArtInfo(artListe, TripAnalysisResultFactory(art, analyseType)));

                    if (analyseType == AnalyseTyper.FoersteObsIAar && FoersteObsIAar(vTur, art.Id))
                        resultList.Add(PopulateArtInfo(artListe, TripAnalysisResultFactory(art, analyseType)));
                }
            });

            return resultList;
        }

        private static TripAnalysisResult PopulateArtInfo(IEnumerable<Art> artListe, TripAnalysisResult analyseResultat)
        {
            var art = artListe.Single(a => a.Id == analyseResultat.ArtId);
            analyseResultat.ArtNavn = art.Navn ?? string.Empty;
            analyseResultat.Speciel = art.Speciel;
            analyseResultat.SU = art.SU;
            return analyseResultat;
        }

        private bool FoersteObsIDatabasen(long fugleturId, long artId) =>
            !analyseQuery.AnalyseData(fugleturId, artId).Any();

        private bool FoersteObsIDK(long fugleturId, long artId) =>
            !analyseQuery.AnalyseData(fugleturId, artId).Any(q => q.RegionId > 0);

        private bool FoersteObsIRegion(VTur vTur, long artId) =>
            !analyseQuery.AnalyseData(vTur.Id, artId).Any(q => q.RegionId == vTur.RegionId);

        private bool FoersteObsForKommune(VTur vTur, long artId) =>
            !analyseQuery.AnalyseData(vTur.Id, artId).Any(q => q.KommuneId == vTur.KommuneId && vTur.KommuneId > 0);

        private bool FoersteObsForLokalitet(VTur vTur, long artId) =>
            !analyseQuery.AnalyseData(vTur.Id, artId).Any(q => q.LokalitetId == vTur.LokalitetId);

        private bool FoersteObsIAar(VTur vTur, long artId) =>
            !analyseQuery.AnalyseData(vTur.Id, artId).Any(q => q.Dato.HasValue && 
                                                               q.Dato.Value.Year == vTur.Aarstal && 
                                                               q.RegionId > 0);

        private bool FoersteObsIMaaned(VTur vTur, long artId) =>
            !analyseQuery.AnalyseData(vTur.Id, artId).Any(q => q.Dato.HasValue && q.Dato.Value.Month == vTur.Maaned);

        private static TripAnalysisResult TripAnalysisResultFactory(Art art, AnalyseTyper analyseType) =>
            new() { AnalyseType = analyseType, ArtId = art.Id };
    }
}