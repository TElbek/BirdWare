using BirdWare.Domain.Models;

namespace BirdWare.Interfaces
{
    public interface IFugleturAnalyseHandler
    {
        IEnumerable<TripAnalysisResult> Analyser(long fugleturId);
    }
}
