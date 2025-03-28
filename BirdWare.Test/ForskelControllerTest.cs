using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test
{
    public class ForskelControllerTest
    {
        private readonly Mock<IForskelQueries> forskelQueriesMock = new();
        private readonly ForskelController forskelController;

        public ForskelControllerTest()
        {
            forskelQueriesMock.Setup(x => x.GetForskelIAar()).Returns([]);
            forskelQueriesMock.Setup(x => x.GetForskelSidsteAar()).Returns([]);
            forskelController = new ForskelController(forskelQueriesMock.Object);
        }

        [Fact]
        public void IAarTest()
        { 
            forskelController.ForskelIAar();
            forskelQueriesMock.Verify(x => x.GetForskelIAar(), Times.Once);
            forskelQueriesMock.Verify(x => x.GetForskelSidsteAar(), Times.Never);
        }

        [Fact]
        public void SidsteAarTest()
        {
            forskelController.ForskelSidsteAar();
            forskelQueriesMock.Verify(x => x.GetForskelSidsteAar(), Times.Once);
            forskelQueriesMock.Verify(x => x.GetForskelIAar(), Times.Never);
        }
    }
}