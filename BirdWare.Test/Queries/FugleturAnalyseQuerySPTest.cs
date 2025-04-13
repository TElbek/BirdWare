using BirdWare.Domain.Models;
using BirdWare.EF.Queries;
using BirdWare.Test.Moq;

namespace BirdWare.Test.Queries
{
    public class FugleturAnalyseQuerySPTest : DbContextMock
    {
        private readonly DbSetMock<SpTripAnalysisResult> spTripAnalysisResultMockSet = new();

        [Fact]
        public void AnalyserTest()
        {
            var fugleturAnalyseQuerySP = GetFugleturAnalyseQuerySP();
            //fugleturAnalyseQuerySP.Analyser(1); -- Mangler en måde at mocke FromSQLRaw
        }

        private FugleturAnalyseQuerySP GetFugleturAnalyseQuerySP()
        {
            MockContext.Setup(c => c.SpTripAnalysisResult).Returns(spTripAnalysisResultMockSet.DbSet);
            return new FugleturAnalyseQuerySP(MockContext.Object);
        }
    }
}
