using BirdWare.Controllers;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Moq;
using System.Net;

namespace BirdWare.Test.Controllers
{
    public class OpretObsControllerTest
    {
        private Mock<IOpretObsCommand> opretObsCommandMock = new();
        private Mock<IOpdaterObsCommand> opdaterObsCommandMock = new();
        private OpretObsController opretObsController;

        public OpretObsControllerTest()
        {
            opretObsCommandMock.Setup(x => x.OpretObsPåFugletur(It.IsAny<long>())).Returns(true);
            opdaterObsCommandMock.Setup(x => x.OpdaterObservation(It.IsAny<VObs>())).Returns(true);
            opretObsController = new OpretObsController(opretObsCommandMock.Object, opdaterObsCommandMock.Object);
        }

        [Fact]
        public void OpretObs_VerifyAuthorizeAttributeClassLevel()
        {
            var type = opretObsController.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            Assert.Empty(attributes);
        }

        [Fact]
        public void OpretObs_VerifyAuthorizeAttribute()
        {
            var type = opretObsController.GetType();
            var methodInfo = type.GetMethod("OpretObs", [typeof(long)]);
            var attributes = methodInfo?.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            Assert.NotNull(attributes);
            Assert.NotEmpty(attributes);
        }

        [Fact]
        public void OpretObs_WithArtIdGreaterThanZero_ReturnsOk()
        {
            var result = opretObsController.OpretObs(1);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            opretObsCommandMock.Verify(x => x.OpretObsPåFugletur(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void OpretObs_WithArtIdGreaterThanZero_Fails()
        {
            opretObsCommandMock.Setup(x => x.OpretObsPåFugletur(It.IsAny<long>())).Returns(false);
            opretObsController = new OpretObsController(opretObsCommandMock.Object, opdaterObsCommandMock.Object);

            var result = opretObsController.OpretObs(1);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            opretObsCommandMock.Verify(x => x.OpretObsPåFugletur(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public void OpretObs_WithArtIdEqualZero_ReturnsBadRequest()
        {
            var result = opretObsController.OpretObs(-1);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            opretObsCommandMock.Verify(x => x.OpretObsPåFugletur(It.IsAny<long>()), Times.Never);
        }

        [Fact]
        public void OpdaterObs_WithVObs_ReturnsOk()
        {
            var result = opretObsController.OpdaterObs(new VObs());
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            opdaterObsCommandMock.Verify(x => x.OpdaterObservation(It.IsAny<VObs>()), Times.Once);
        }

        [Fact]
        public void OpdaterObs_WithVObs_Fails()
        {
            opdaterObsCommandMock.Setup(x => x.OpdaterObservation(It.IsAny<VObs>())).Returns(false);
            opretObsController = new OpretObsController(opretObsCommandMock.Object, opdaterObsCommandMock.Object);

            var result = opretObsController.OpdaterObs(new VObs());
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            opdaterObsCommandMock.Verify(x => x.OpdaterObservation(It.IsAny<VObs>()), Times.Once);
        }
    }
}
