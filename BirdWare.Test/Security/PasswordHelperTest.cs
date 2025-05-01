using BirdWare.Domain.Entities;
using BirdWare.Domain.Security;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BirdWare.Test.Security
{
    public class PasswordHelperTest
    {
        private readonly Mock<IPasswordHasher<Bruger>> passsWordHasherMock = new();
        private PasswordHelper? passwordHelper;

        private void Success()
        {
            passsWordHasherMock.Setup(x => x.HashPassword(It.IsAny<Bruger>(), It.IsAny<string>()))
                .Returns("hashedPassword");

            passsWordHasherMock.Setup(x => x.VerifyHashedPassword(It.IsAny<Bruger>(), It.IsAny<string>(), It.IsAny<string>())).Returns(PasswordVerificationResult.Success);
            passwordHelper = new PasswordHelper(passsWordHasherMock.Object);
        }

        private void Failure()
        {
            passsWordHasherMock.Setup(x => x.VerifyHashedPassword(It.IsAny<Bruger>(), "NotValid", "NotValid")).Returns(PasswordVerificationResult.Success);
            passwordHelper = new PasswordHelper(passsWordHasherMock.Object);
        }

        [Fact]
        public void HashPassword_ShouldReturnHashedPassword()
        {
            Success();
            var user = new Bruger();
            var password = "password";

            var result = passwordHelper?.HashPassword(user, password);
            Assert.Equal("hashedPassword", result);
            passsWordHasherMock.Verify(x => x.HashPassword(user, password), Times.Once);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
        {
            Success();
            var user = new Bruger();
            var hashedPassword = "hashedPassword";
            var password = "password";
            var result = passwordHelper?.VerifyPassword(user, hashedPassword, password);
            Assert.True(result);
            passsWordHasherMock.Verify(x => x.VerifyHashedPassword(user, hashedPassword, password), Times.Once);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsNotValid()
        {
            Failure();
            var user = new Bruger() {UserName = "NotValid"};
            var password = "NotValid";
            var hashedPassword = "hashedPassword";

            var result = passwordHelper?.VerifyPassword(user, hashedPassword, password);
            Assert.False(result);
            passsWordHasherMock.Verify(x => x.VerifyHashedPassword(user, hashedPassword, password), Times.Once);
        }

    }
}
