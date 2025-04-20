using BirdWare.Domain.Entities;

namespace BirdWare.Domain.Interfaces
{
    public interface ITokenHelper
    {
        string GenerateJWTToken(Bruger bruger);
    }
}
