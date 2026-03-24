using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class ForskelControllerTest
    {
        private readonly Mock<IForskelQueries> forskelQueriesMock = new();
        private readonly ForskelController forskelController;

        public ForskelControllerTest()
        {
            forskelQueriesMock.Setup(x => x.GetForskelIAar().Result).Returns([]);
            forskelQueriesMock.Setup(x => x.GetForskelSidsteAar().Result).Returns([]);
            forskelController = new ForskelController(forskelQueriesMock.Object);
        }

        [Fact]
        public async Task IAarTest()
        {
            await forskelController.ForskelIAar();
            forskelQueriesMock.Verify(x => x.GetForskelIAar(), Times.Once);
            forskelQueriesMock.Verify(x => x.GetForskelSidsteAar(), Times.Never);
        }

        [Fact]
        public async Task SidsteAarTest()
        {
            await forskelController.ForskelSidsteAar();
            forskelQueriesMock.Verify(x => x.GetForskelSidsteAar(), Times.Once);
            forskelQueriesMock.Verify(x => x.GetForskelIAar(), Times.Never);
        }
    }
}