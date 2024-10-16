using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Entities.IdentityEntities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.TokenServices
{
    public class TokenServices : ITokenServices

    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>

            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName, appUser.DisplayName),
                new Claim("UserId" , appUser.Id),
                new Claim("UserName" , appUser.UserName),

            };

            var cred = new SigningCredentials(_key , SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _config["Token:Issuer"],
                IssuedAt = DateTime.Now,  
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = cred,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
