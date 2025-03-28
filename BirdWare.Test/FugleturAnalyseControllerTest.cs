using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test
{
    public class FugleturAnalyseControllerTest
    {
        private readonly Mock<IFugleturAnalyseQuerySP> fugleturAnalyseQuerySP = new();
        private readonly FugleturAnalyseController fugleturAnalyseController;

        public FugleturAnalyseControllerTest()
        {
            fugleturAnalyseQuerySP.Setup(x => x.Analyser(It.IsAny<long>())).Returns([]);
            fugleturAnalyseController = new FugleturAnalyseController(fugleturAnalyseQuerySP.Object);
        }

        [Fact]
        public void AnalyserFugleturTest()
        {
            fugleturAnalyseController.AnalyserFugletur(0);
            fugleturAnalyseQuerySP.Verify(x => x.Analyser(It.IsAny<long>()), Times.Once);
        }
    }
}
