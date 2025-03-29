using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class FugleturControllerTest
    {
        private readonly Mock<IFugleturQuery> fugleturQueryMock = new();
        private readonly FugleturController fugleturController;

        public FugleturControllerTest()
        {
            fugleturQueryMock.Setup(x => x.GetFugletur(It.IsAny<long>())).Returns(new VTur());
            fugleturQueryMock.Setup(x => x.GetSenesteFugletur()).Returns(0);
            fugleturController = new FugleturController(fugleturQueryMock.Object);
        }

        [Fact]
        public void GetFugleturTest()
        {
            fugleturController.GetFugletur(1);
            fugleturQueryMock.Verify(x => x.GetFugletur(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void GetSenesteFugleturIdTest()
        {
            fugleturController.GetSenesteFugleturId();
            fugleturQueryMock.Verify(x => x.GetSenesteFugletur(), Times.Once);
        }
    }
}
