using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BirdWare.EF.Queries
{
    public class FugleturAnalyseQuerySP(BirdWareContext birdWareContext) : IFugleturAnalyseQuerySP
    {
        public List<SpTripAnalysisResult> Analyser(long fugleturId)
        {
            var sqlParameter = new SqlParameter("@TripId", fugleturId);
            return [.. birdWareContext.SpTripAnalysisResult.FromSqlRaw("spTripAnalysis @TripId", sqlParameter)];
        }
    }
}