using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Teste_TOPT_Swagger.Models;
using Teste_TOPT_Swagger.Service.IService;


namespace Teste_TOPT_Swagger.Service
{

    public class LoginService : ILoginService
    {
        private readonly ConfigJwt _configJwt;

        public LoginService(IOptions<ConfigJwt> configJwt)
        {
            _configJwt = configJwt.Value;
        }
        public bool AuthLogin(string username, string password)
        {
            return username == "teste" && password == "password";
        }

        public string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configJwt.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configJwt.Issuer,
                Audience = _configJwt.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        
        }
    }
}
