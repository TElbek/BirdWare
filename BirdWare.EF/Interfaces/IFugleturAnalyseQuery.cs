using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuery
    {
        Task<IEnumerable<TripAnalysisResult>> Analyser(long fugleturId);
    }
}
