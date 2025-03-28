using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class LokalitetControllerTest
    {
        private readonly Mock<ILokaliteterByLatLongQuerySP> lokaliteterByLatLongQuerySPMock = new();
        private readonly LokalitetController lokalitetController;

        public LokalitetControllerTest()
        {
            lokaliteterByLatLongQuerySPMock.Setup(x => x.FindLokaliteterLatLong(It.IsAny<double>(), It.IsAny<double>())).Returns([]);
            lokalitetController = new LokalitetController(lokaliteterByLatLongQuerySPMock.Object);
        }

        [Fact]
        public void FindLokaliteterUdfraLatLongTest()
        {
            lokalitetController.FindLokaliteterUdfraLatLong(0, 0);
            lokaliteterByLatLongQuerySPMock.Verify(x => x.FindLokaliteterLatLong(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
        }
    }
}
