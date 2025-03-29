using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class AaretsgangControllerTest
    {
        private readonly Mock<IAaretsGangQuery> aaretsGangQueryMock = new();
        private readonly AaretsgangController aaretsgangController;

        public AaretsgangControllerTest()
        {
            aaretsGangQueryMock.Setup(x => x.GetAaretsGang()).Returns([]);
            aaretsgangController = new AaretsgangController(aaretsGangQueryMock.Object);
        }

        [Fact]
        public void GetAaretsGangTest()
        {
            aaretsgangController.GetAaretsGang();
            aaretsGangQueryMock.Verify(x => x.GetAaretsGang(), Times.Once);
        }
    }
}
