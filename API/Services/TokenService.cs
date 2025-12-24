using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using DatingApp.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = configuration["TokenKey"] ?? throw new InvalidOperationException("Token key is not configured.");

        if (tokenKey.Length < 64)
        {
            throw new InvalidOperationException("Token key must be at least 64 characters long.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
