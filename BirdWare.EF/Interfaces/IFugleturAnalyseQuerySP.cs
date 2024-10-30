using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuerySP
    {
        List<SpTripAnalysisResult> Analyser(long fugleturId);
    }
}
