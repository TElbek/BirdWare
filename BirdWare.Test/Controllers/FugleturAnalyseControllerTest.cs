using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class FugleturAnalyseControllerTest
    {
        private readonly Mock<IFugleturAnalyseQuery> fugleturAnalyseQuery = new();
        private readonly FugleturAnalyseController fugleturAnalyseController;

        public FugleturAnalyseControllerTest()
        {
            fugleturAnalyseQuery.Setup(x => x.Analyser(It.IsAny<long>())).Returns([]);
            fugleturAnalyseController = new FugleturAnalyseController(fugleturAnalyseQuery.Object);
        }

        [Fact]
        public void AnalyserFugleturTest()
        {
            fugleturAnalyseController.AnalyserFugletur(0);
            fugleturAnalyseQuery.Verify(x => x.Analyser(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void GetAnalyseTypeListeTest()
        {
            var result = fugleturAnalyseController.GetAnalyseTypeListe();
            Assert.NotNull(result);
            Assert.IsType<List<AnalyseTypeModel>>(result);
            Assert.Equal(Enum.GetNames(typeof(AnalyseTyper)).Length, result.Count);
        }
    }
}
