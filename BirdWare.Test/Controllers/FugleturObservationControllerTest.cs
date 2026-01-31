using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class FugleturObservationControllerTest
    {
        private readonly Mock<IFugleturQuery> fugleturQueryMock = new();
        private readonly Mock<IFugleturObservationQuery> fugleturObservationQueryMock = new();
        private readonly Mock<ISletObsCommand> sletObsCommandMock = new();
        private readonly FugleturObservationController fugleturObservationController;

        public FugleturObservationControllerTest()
        {
            fugleturObservationQueryMock.Setup(x => x.GetObservationer(It.IsAny<long>())).Returns([]);
            sletObsCommandMock.Setup(x => x.SletObservation(It.IsAny<long>()));
            fugleturQueryMock.Setup(x => x.GetSenesteFugletur()).Returns(1);
            fugleturObservationController = new FugleturObservationController(fugleturQueryMock.Object, fugleturObservationQueryMock.Object, sletObsCommandMock.Object);
        }

        [Fact]
        public void GetFugleturObservationerTest()
        {
            fugleturObservationController.GetFugleturObservationer(1);
            fugleturObservationQueryMock.Verify(x => x.GetObservationer(1), Times.Once);
        }

        [Fact]
        public void GetSenesteFugleturObservationerTest()
        {
            fugleturObservationController.GetSenesteFugleturObservationer();
            fugleturQueryMock.Verify(x => x.GetSenesteFugletur(), Times.Once);
            fugleturObservationQueryMock.Verify(x => x.GetObservationer(1), Times.Once);
        }

        [Fact]
        public void SletObservationTest()
        {
            fugleturObservationController.SletObservation(1);
            sletObsCommandMock.Verify(x => x.SletObservation(1), Times.Once);
        }
    }
}
