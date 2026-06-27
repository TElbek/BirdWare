using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;

namespace BirdWare.EF.Security
{
    public class LoginHelper(IPasswordHelper passwordHelper, IBrugerQuery brugerQuery) : ILoginHelper
    {
        public bool DoLogin(LoginModel loginModel)
        {
            ArgumentNullException.ThrowIfNull(loginModel);
            if (loginModel.HasNoLoginInformation) return false;

            try
            {
                var bruger = brugerQuery.GetBrugerByName(loginModel?.Username ?? string.Empty);
                return passwordHelper.VerifyPassword(bruger, bruger.PasswordHash, loginModel?.Password ?? string.Empty);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
