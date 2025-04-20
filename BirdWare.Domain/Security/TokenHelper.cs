using BirdWare.Domain.Entities;
using BirdWare.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BirdWare.Domain.Security
{
    public class TokenHelper : ITokenHelper
    {
        public string GenerateJWTToken(Bruger bruger)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, bruger.UserName + "-" + bruger.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var symmetricSecurityKey = GetSymmetricSecurityKey();
            var signingCredentials = GetSigningCredentials(symmetricSecurityKey);
            var token = GetJwtSecurityToken(claims, signingCredentials);

            return WriteToken(token);
        }

        private static string WriteToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static JwtSecurityToken GetJwtSecurityToken(Claim[] claims, SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: "birdware.dk",
                audience: "birdware.dk",
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signingCredentials);
        }

        private static SigningCredentials GetSigningCredentials(SymmetricSecurityKey symmetricSecurityKey)
        {
            return new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("appSecret") ?? string.Empty));
        }
    }
}