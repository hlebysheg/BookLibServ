using BookLibServ.Models.EntityModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WordBook.service.Interface;

namespace WordBook.service.Service
{
    public class GeneratorJWTService: IGenerateJWT
    {
        IConfiguration _conf;
        public GeneratorJWTService(IConfiguration config)
        {
            _conf = config;
        }
        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_conf["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var token = new JwtSecurityToken(_conf["Jwt:Issuer"],
                _conf["Jwt:Audience"],
                claims, expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRandomStr(int len)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[len];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
