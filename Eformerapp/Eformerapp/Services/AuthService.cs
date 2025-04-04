// Services/AuthService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Eformerapp.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Eformerapp.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("mobileNumber", user.MobileNumber),
                new Claim("address1", user.Address1 ?? string.Empty),
                new Claim("address2", user.Address2 ?? string.Empty),
                new Claim("address3", user.Address3 ?? string.Empty),
                new Claim("city", user.City ?? string.Empty),
                new Claim("pincode", user.Pincode ?? string.Empty),
                new Claim("userRoleId", user.UserRoleId.ToString()),
                new Claim("userRoleName", user.UserRole?.Name ?? string.Empty)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}