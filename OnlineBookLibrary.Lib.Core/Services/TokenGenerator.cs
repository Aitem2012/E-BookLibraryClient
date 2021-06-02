using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineBookLibrary.Lib.Core.Interfaces;
using OnlineBookLibraryClient.Lib.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookLibrary.Lib.Core.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtConfig _jwtConfig;
        private readonly UserManager<AppUser> _userManager;
        
        public TokenGenerator(UserManager<AppUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        public async Task<string> GenerateToken(AppUser user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                rolesClaims.Add(new Claim("roles", roles[i]));
            }
            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                .Union(rolesClaims)
                ),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var Token = JwtTokenHandler.CreateToken(TokenDescriptor);
            var JwtToken = JwtTokenHandler.WriteToken(Token);
            return JwtToken;
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            if (string.IsNullOrEmpty(token)){
                throw new ArgumentException("Token is null or Empty");
            }
            TokenValidationParameters tokenValidation = GetTokenValidationParameters();
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidation, out SecurityToken validatedToken);

                return tokenValid.Claims;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Encoding.ASCII.GetBytes(_jwtConfig.Secret); // Convert.FromBase64String(_jwtConfig.Secret);
            return new SymmetricSecurityKey(symmetricKey);
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
        
    }
}
