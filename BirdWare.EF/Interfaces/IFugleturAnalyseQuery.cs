using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuery
    {
        IQueryable<long> HentArtListe(long fugleturId);
        VTur FindFugletur(long fugleturId);

        IQueryable<TripAnalysisResult>  FoersteObsIDatabasen(VTur vTur,IQueryable<long> arterForTuren);
        IQueryable<TripAnalysisResult> FoersteObsIDK(VTur vTur,IQueryable<long> arterForTuren);
        IQueryable<TripAnalysisResult> FoersteObsIRegion(VTur vTur,IQueryable<long> arterForTuren);
        IQueryable<TripAnalysisResult> FoersteObsForKommune(VTur vTur,IQueryable<long> arterForTuren);
        IQueryable<TripAnalysisResult> FoersteObsForLokalitet(VTur vTur,IQueryable<long> arterForTuren);
        IQueryable<TripAnalysisResult> FoersteObsIAar(VTur vTur,IQueryable<long> arterForTuren);
        IQueryable<TripAnalysisResult> FoersteObsIMaaned(VTur vTur,IQueryable<long> arterForTuren);
    }
}
