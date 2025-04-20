using BirdWare.Domain.Entities;

namespace BirdWare.Domain.Interfaces
{
    public interface IPasswordHelper
    {
        string HashPassword(Bruger user, string password);
        bool VerifyPassword(Bruger user, string hashedPassword, string password);
    }
}
