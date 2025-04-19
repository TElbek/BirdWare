using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    public class LoginController(ILoginHelper loginHelper, 
                                 ITokenHelper tokenHelper,
                                 IBrugerQuery brugerQuery) : ControllerBase
    {
        [HttpPost]
        [Route("api/auth/login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginHelper.DoLogin(loginModel))
            {
                var bruger = brugerQuery.GetBrugerByName(loginModel.Username);
                var token = tokenHelper.GenerateJwtToken(bruger);
                return Ok(new { AccessToken = token });
            }
            else 
            { 
                return Unauthorized();
            }
        }
    }
}