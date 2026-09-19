using BirdWare.Domain.Models;
using BirdWare.EF;
using BirdWare.EF.Interfaces;
using BirdWare.Interfaces;

namespace BirdWare.Business
{
    public class FugleturAnalyseHandler(BirdWareContext birdWareContext, IFugleturAnalyseQuery analyseQuery) : IFugleturAnalyseHandler
    {
        public IEnumerable<TripAnalysisResult> Analyser(long fugleturId, AnalyseTyper analyseType)
        {
            var vTur = analyseQuery.FindFugletur(fugleturId);
            var artsListe = analyseQuery.HentArtListe(fugleturId);

            return analyseType switch
            {
                AnalyseTyper.FoersteObsIDatabasen => [.. analyseQuery.FoersteObsIDatabasen(vTur, artsListe)],
                AnalyseTyper.FoersteObsIDK => [.. analyseQuery.FoersteObsIDK(vTur, artsListe)],
                AnalyseTyper.FoersteObsIRegion => [.. analyseQuery.FoersteObsIRegion(vTur, artsListe)],
                AnalyseTyper.FoersteObsForKommune => [.. analyseQuery.FoersteObsForKommune(vTur, artsListe)],
                AnalyseTyper.FoersteObsForLokalitet => [.. analyseQuery.FoersteObsForLokalitet(vTur, artsListe)],
                AnalyseTyper.FoersteObsIAar => [.. analyseQuery.FoersteObsIAar(vTur, artsListe)],
                AnalyseTyper.FoersteObsIMaaned => [.. analyseQuery.FoersteObsIMaaned(vTur, artsListe)],
                _ => throw new NotImplementedException()
            };
        }
    }
}