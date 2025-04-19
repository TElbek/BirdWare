using BirdWare.Domain.Models;

namespace BirdWare.Domain.Interfaces
{
    public interface ITokenHelper
    {
        string GenerateJwtToken(Bruger bruger);
    }
}
