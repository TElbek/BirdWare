using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class SynchControllerTest
    {
        private readonly Mock<ISynchTripCommand> syncTripCommand = new();
        private readonly Mock<ISynchTripQuery> syncTripquery = new();
        private SynchController synchController;

        public SynchControllerTest()
        {
            syncTripquery.Setup(x => x.GetTrip(It.IsAny<long>())).Returns(new SynchTrip());
            syncTripCommand.Setup(x => x.PostTrip(It.IsAny<SynchTrip>())).Returns(true);
            synchController = new SynchController(syncTripCommand.Object, syncTripquery.Object);
        }

        [Fact]
        public void GetTripAndObservationsTest()
        {
            synchController.GetTripAndObservations(1);
            syncTripquery.Verify(x => x.GetTrip(1), Times.Once);
        }

        [Fact]
        public void AddTripTest()
        {
            var response = synchController.AddTrip(new SynchTrip { Fugletur = new SynchFugletur { FugleturId = 1 } });
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            syncTripCommand.Verify(x => x.PostTrip(It.IsAny<SynchTrip>()), Times.Once);
        }

        [Fact]
        public void AddTripTestFugleturIdZero()
        {
            var response = synchController.AddTrip(new SynchTrip { Fugletur = new SynchFugletur { FugleturId = 0 } });
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            syncTripCommand.Verify(x => x.PostTrip(It.IsAny<SynchTrip>()), Times.Never);
        }

        [Fact]
        public void AddTripTestFugleturNull()
        {
            syncTripCommand.Setup(x => x.PostTrip(It.IsAny<SynchTrip>())).Returns(false);
            synchController = new SynchController(syncTripCommand.Object, syncTripquery.Object);

            var response = synchController.AddTrip(new SynchTrip());
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            syncTripCommand.Verify(x => x.PostTrip(It.IsAny<SynchTrip>()), Times.Never);
        }
    }
}
