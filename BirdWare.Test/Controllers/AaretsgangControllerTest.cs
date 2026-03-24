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
            aaretsGangQueryMock.Setup(x => x.GetAaretsGang().Result).Returns([]);
            aaretsgangController = new AaretsgangController(aaretsGangQueryMock.Object);
        }

        [Fact]
        public async Task GetAaretsGangTest()
        {
            await aaretsgangController.GetAaretsGang();
            aaretsGangQueryMock.Verify(x => x.GetAaretsGang(), Times.Once);
        }
    }
}
