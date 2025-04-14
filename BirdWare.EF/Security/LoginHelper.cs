using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Security
{
    public class LoginHelper(IPasswordHelper passwordHelper, IBrugerQuery brugerQuery) : ILoginHelper
    {
        public bool DoLogin(LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return false;
            }

            try
            {
                var bruger = brugerQuery.GetBrugerByName(loginModel.Username);
                if (passwordHelper.VerifyPassword(bruger, bruger.PasswordHash, loginModel.Password))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
    }
}
