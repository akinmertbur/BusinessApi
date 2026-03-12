using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessApi.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BusinessApi.Services {
    public class TokenService : ITokenService {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) {
            _configuration = configuration;
        }

        public string CreateToken(User user) {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = jwtSettings["Key"]!;
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiresInMinutes = int.Parse(jwtSettings["ExpiresInMinutes"]!);


            var claims = new List<Claim> {
                new Claim("userId", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
