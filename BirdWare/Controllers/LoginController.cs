using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirdWare.Controllers
{
    public class LoginController(ILoginHelper loginHelper) : ControllerBase
    {
        [HttpPost]
        [Route("api/auth/login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            return loginHelper.DoLogin(loginModel) ? 
                    Ok("Login successful.") : 
                    BadRequest("Invalid login request.");
        }
    }
}
