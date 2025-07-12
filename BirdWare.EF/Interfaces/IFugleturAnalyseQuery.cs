using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface IFugleturAnalyseQuery
    {
        Task<List<TripAnalysisResult>> Analyser(long fugleturId);
    }
}
