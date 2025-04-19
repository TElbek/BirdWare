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
            var brugerQueriesMock = BrugerQueriesFactory();

            var loginController = ControllerFactory(brugerQueriesMock, loginHelperMock, tokenHelperMock);

            var loginModel = new LoginModel
            {
                Username = "testUser",
                Password = "testPassword"
            };
            var result = loginController.Login(loginModel);
            Assert.IsType<UnauthorizedResult>(result);
            loginHelperMock.Verify(x => x.DoLogin(loginModel), Times.Once);
            tokenHelperMock.Verify(x => x.GenerateJwtToken(It.IsAny<Bruger>()), Times.Never);
        }

        [Fact]
        public void LoginTestOK()
        {
            var tokenHelperMock = TokenHelperMockFactory();
            var loginHelperMock = LoginHelperMockFactory(true);
            var brugerQueriesMock = BrugerQueriesFactory();

            var loginController = ControllerFactory(brugerQueriesMock, loginHelperMock, tokenHelperMock);

            var loginModel = new LoginModel
            {
                Username = "testUser",
                Password = "testPassword"
            };
            var result = loginController.Login(loginModel);
            Assert.IsType<OkObjectResult>(result);
            loginHelperMock.Verify(x => x.DoLogin(loginModel), Times.Once);
            tokenHelperMock.Verify(x => x.GenerateJwtToken(It.IsAny<Bruger>()), Times.Once);
        }

        private static LoginController ControllerFactory(
            Mock<IBrugerQuery> brugerQueriesMock,
            Mock<ILoginHelper> loginHelperMock,
            Mock<ITokenHelper> tokenHelperMock)
        {
            LoginController loginController;
            loginController = new LoginController(loginHelperMock.Object, tokenHelperMock.Object, brugerQueriesMock.Object);
            return loginController;
        }

        private static Mock<IBrugerQuery> BrugerQueriesFactory()
        {
            var brugerQueriesMock = new Mock<IBrugerQuery>();
            brugerQueriesMock.Setup(x => x.GetBrugerByName(It.IsAny<string>()))
                .Returns(new Bruger
                {
                    Id = 12,
                    UserName = "testUser",
                    PasswordHash = "hashedPassword"
                });

            return brugerQueriesMock;
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
            tokenHelperMock.Setup(x => x.GenerateJwtToken(It.IsAny<Bruger>())).Returns("123aaa");
            return tokenHelperMock;
        }
    }
}
