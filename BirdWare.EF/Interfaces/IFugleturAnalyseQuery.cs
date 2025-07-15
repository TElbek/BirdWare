using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuery
    {
        List<TripAnalysisResult> Analyser(long fugleturId);
    }
}
