namespace BirdWare.Domain.Interfaces
{
    public interface ITokenHelper
    {
        string GenerateJwtToken(string username);
    }
}
