using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BirdWare.Domain.Security
{
    public class TokenHelper : ITokenHelper
    {
        public string GenerateJwtToken(Bruger bruger)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, bruger.UserName + "-" + bruger.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("appSecret") ?? string.Empty));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "birdware.dk",
                audience: "birdware.dk",
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
