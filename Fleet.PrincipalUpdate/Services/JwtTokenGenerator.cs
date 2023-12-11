using Fleet.PrincipalUpdate.Models;
using Fleet.PrincipalUpdate.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fleet.PrincipalUpdate.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(AplicationUser aplicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var ClaimList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, aplicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, aplicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, aplicationUser.UserName)

            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(ClaimList),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
