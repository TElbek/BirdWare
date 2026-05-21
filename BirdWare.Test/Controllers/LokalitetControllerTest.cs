using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class LokalitetControllerTest
    {
        private readonly Mock<ILokaliteterByLatLongQuery> lokaliteterByLatLongQueryMock = new();
        private readonly LokalitetController lokalitetController;

        public LokalitetControllerTest()
        {
            lokaliteterByLatLongQueryMock.Setup(x => x.FindLokaliteterLatLong(It.IsAny<double>(), It.IsAny<double>())).Returns([]);
            lokalitetController = new LokalitetController(lokaliteterByLatLongQueryMock.Object);
        }

        [Fact]
        public void FindLokaliteterUdfraLatLongTest()
        {
            lokalitetController.FindLokaliteterLatLong(0, 0);
            lokaliteterByLatLongQueryMock.Verify(x => x.FindLokaliteterLatLong(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
        }
    }
}
