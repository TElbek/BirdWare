using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuery
    {
        IEnumerable<TripAnalysisResult> Analyser(long fugleturId);
    }
}
