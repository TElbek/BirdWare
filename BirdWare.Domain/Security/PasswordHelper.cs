using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace BirdWare.Domain.Security
{
    public class PasswordHelper(IPasswordHasher<Bruger> passwordHasher) : IPasswordHelper
    {
        private readonly IPasswordHasher<Bruger> passwordHasher = passwordHasher;

        public string HashPassword(Bruger user, string password)
        {
            return passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(Bruger user, string hashedPassword, string password)
        {
            var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
