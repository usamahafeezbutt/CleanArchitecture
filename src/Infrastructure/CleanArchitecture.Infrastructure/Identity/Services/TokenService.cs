

using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models.Identity;
using CleanArchitecture.Application.Common.Models.Response;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public Response<AuthResponse> GenerateUserToken(ApplicationUser user)
        =>  new()
            {
                Success = true,
                Message = "Token Generated Successfull",
                Result = new AuthResponse
                {
                    Token = PrepareAuthToken(user),
                    Email = user.Email,
                    Expiry = _jwtSettings.Expiry
                }
            };

        private string PrepareAuthToken(ApplicationUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = PrepareTokenDescritpion(user, key);
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor PrepareTokenDescritpion(ApplicationUser user, byte[] key)
        => new ()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddHours(_jwtSettings.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
    }
}
