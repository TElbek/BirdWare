using BirdWare.Domain.Models;

namespace BirdWare.EF.Interfaces
{
    public interface ILoginHelper
    {
        bool DoLogin(LoginModel loginModel);
    }
}
