using BirdWare.Controllers;
using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BirdWare.Test.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public void LoginTestFail()
        {
            var tokenHelperMock = TokenHelperMockFactory();
            var loginHelperMock = LoginHelperMockFactory(false);
            var loginController = ControllerFactory(loginHelperMock, tokenHelperMock);

            var loginModel = new LoginModel
            {
                Username = "testUser",
                Password = "testPassword"
            };
            var result = loginController.Login(loginModel);
            Assert.IsType<UnauthorizedResult>(result);
            loginHelperMock.Verify(x => x.DoLogin(loginModel), Times.Once);
            tokenHelperMock.Verify(x => x.GenerateJwtToken(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void LoginTestOK()
        {
            var tokenHelperMock = TokenHelperMockFactory();
            var loginHelperMock = LoginHelperMockFactory(true);
            var loginController = ControllerFactory(loginHelperMock, tokenHelperMock);

            var loginModel = new LoginModel
            {
                Username = "testUser",
                Password = "testPassword"
            };
            var result = loginController.Login(loginModel);
            Assert.IsType<OkObjectResult>(result);
            loginHelperMock.Verify(x => x.DoLogin(loginModel), Times.Once);
            tokenHelperMock.Verify(x => x.GenerateJwtToken(It.IsAny<string>()), Times.Once);
        }

        private static LoginController ControllerFactory(
            Mock<ILoginHelper> loginHelperMock,
            Mock<ITokenHelper> tokenHelperMock)
        {
            LoginController loginController;
            loginController = new LoginController(loginHelperMock.Object, tokenHelperMock.Object);
            return loginController;
        }

        private static Mock<ILoginHelper> LoginHelperMockFactory(bool mockValue)
        {
            var loginHelperMock = new Mock<ILoginHelper>();
            loginHelperMock.Setup(x => x.DoLogin(It.IsAny<LoginModel>())).Returns(mockValue);
            return loginHelperMock;
        }

        private static Mock<ITokenHelper> TokenHelperMockFactory()
        {
            var tokenHelperMock = new Mock<ITokenHelper>();
            tokenHelperMock.Setup(x => x.GenerateJwtToken(It.IsAny<string>())).Returns("123aaa");
            return tokenHelperMock;
        }
    }
}
