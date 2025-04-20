using BirdWare.Controllers;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Moq;
using System.Net;

namespace BirdWare.Test.Controllers
{
    public class OpretTurControllerTest
    {
        private Mock<IOpretTurCommand> opretTurCommandMock = new();
        private OpretTurController opretTurController;

        public OpretTurControllerTest()
        {
            opretTurCommandMock.Setup(x => x.OpretTurPaaLokalitet(It.IsAny<long>())).Returns(true);
            opretTurController = new OpretTurController(opretTurCommandMock.Object);
        }

        [Fact]
        public void OpretTur_VerifyAuthorizeAttribute()
        {
            var type = opretTurController.GetType();
            var attributes = type.GetCustomAttributes(typeof(AuthorizeAttribute), true);
            Assert.NotEmpty(attributes);
        }

        [Fact]
        public void OpretTur_LokalitetIdGreaterThanZero_ReturnsOk()
        {
            var result = opretTurController.OpretTur(1);
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            opretTurCommandMock.Verify(x => x.OpretTurPaaLokalitet(1), Times.Once);
        }

        [Fact]
        public void OpretTur_LokalitetIdNotGreaterThanZero_ReturnsBadRequest()
        {
            var result = opretTurController.OpretTur(-1);
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            opretTurCommandMock.Verify(x => x.OpretTurPaaLokalitet(1), Times.Never);
        }

        [Fact]
        public void OpretTur_FailsToCreateTur_ReturnsBadRequest()
        {
            opretTurCommandMock.Setup(x => x.OpretTurPaaLokalitet(It.IsAny<long>())).Returns(false);
            opretTurController = new OpretTurController(opretTurCommandMock.Object);

            var result = opretTurController.OpretTur(1);
            Assert.IsType<HttpResponseMessage>(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            opretTurCommandMock.Verify(x => x.OpretTurPaaLokalitet(1), Times.Once);
        }
    }
}
