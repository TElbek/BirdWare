using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BirdWare.Controllers
{
    public class LoginController(ILoginHelper loginHelper) : ControllerBase
    {
        [HttpPost]
        [Route("api/auth/login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginHelper.DoLogin(loginModel))
            {
                var token = GenerateJwtToken(loginModel.Username);
                return Ok(new { AccessToken = token });
            }
            else 
            { 
                return Unauthorized();
            }
        }

        private static string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("appSecret") ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:   "birdware.dk",
                audience: "birdware.dk",
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
