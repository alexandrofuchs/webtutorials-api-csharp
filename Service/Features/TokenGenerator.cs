using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Middleware.Features
{
    public static class TokenGenerator
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("First Name", user.FirstName),
                    new Claim("Last Name", user.LastName),
                    new Claim("Email", user.Email),                   
                    new Claim("CreatedAt", user.CreatedAt.ToLongDateString()),
                    new Claim("UpdatedAt", user.UpdatedAt.ToLongDateString()),
                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);           
            return tokenHandler.WriteToken(token);
        }
    }
}
