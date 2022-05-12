using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdvertisingAgency.BLL.Configs;
using Microsoft.IdentityModel.Tokens;

namespace AdvertisingAgency.BLL.Helpers;

public static class JwtHelper
{
    public static string GenerateJwt(string userId, string userEmail, string role, JwtConfig jwtConfig)
    {
        var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new("id", userId),
                new("role", role),
                new("email", userEmail)
            }),
            Expires = DateTime.UtcNow.AddDays(30),
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience,
            SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}