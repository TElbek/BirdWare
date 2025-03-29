using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Moq;
using System;

namespace BirdWare.Test.Controllers
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
