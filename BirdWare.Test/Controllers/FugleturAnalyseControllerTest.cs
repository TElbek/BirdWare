using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class FugleturAnalyseControllerTest
    {
        private readonly Mock<IFugleturAnalyseHandler> fugleturAnalyseHandler = new();
        private readonly FugleturAnalyseController fugleturAnalyseController;

        public FugleturAnalyseControllerTest()
        {
            fugleturAnalyseHandler.Setup(x => x.Analyser(It.IsAny<long>())).Returns([]);
            fugleturAnalyseController = new FugleturAnalyseController(fugleturAnalyseHandler.Object);
        }

        [Fact]
        public void AnalyserFugleturTest()
        {
            fugleturAnalyseController.AnalyserFugletur(1);
            fugleturAnalyseHandler.Verify(x => x.Analyser(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void GetAnalyseTypeListeTest()
        {
            var result = fugleturAnalyseController.GetAnalyseTypeListe();
            Assert.NotNull(result);
            Assert.IsType<List<AnalyseTypeModel>>(result.ToList());
            Assert.Equal(Enum.GetNames(typeof(AnalyseTyper)).Length, result.ToList().Count);
        }
    }
}
