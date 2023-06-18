using API.Domain.User.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Infra.Services
{
    public class JWTTokenService : ITokenService
    {
        private readonly string key;

        public JWTTokenService(string key)
        {
            this.key = key;
        }

        public string Generate(int userId, DateTime expiresAt)
        {
            byte[] bytesKey = Encoding.ASCII.GetBytes(key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", userId.ToString()),
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha512Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
