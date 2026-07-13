using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuerySP
    {
        IEnumerable<SpTripAnalysisResult> Analyser(long fugleturId);
    }
}
