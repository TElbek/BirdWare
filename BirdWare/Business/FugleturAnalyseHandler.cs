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
            var artsListe = analyseQuery.HentArtListe(fugleturId);

            return analyseType switch
            {
                AnalyseTyper.FoersteObsIDatabasen => 
                    [.. analyseQuery.FoersteObsIDatabasen(vTur, artsListe)],

                AnalyseTyper.FoersteObsIDK when vTur.RegionId > 0 => 
                    [.. analyseQuery.FoersteObsIDK(vTur, artsListe)],

                AnalyseTyper.FoersteObsIRegion => 
                    [.. analyseQuery.FoersteObsIRegion(vTur, artsListe)],

                AnalyseTyper.FoersteObsForKommune when vTur.RegionId > 0 && vTur.KommuneId > 0 => 
                    [.. analyseQuery.FoersteObsForKommune(vTur, artsListe)],

                AnalyseTyper.FoersteObsForLokalitet => 
                    [.. analyseQuery.FoersteObsForLokalitet(vTur, artsListe)],

                AnalyseTyper.FoersteObsIAar when vTur.RegionId > 0 => 
                    [.. analyseQuery.FoersteObsIAar(vTur, artsListe)],

                AnalyseTyper.FoersteObsIMaaned when vTur.RegionId > 0 => 
                    [.. analyseQuery.FoersteObsIMaaned(vTur, artsListe)],

                _ => throw new NotImplementedException()
            };
        }
    }
}