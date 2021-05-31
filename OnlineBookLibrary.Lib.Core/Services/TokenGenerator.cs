﻿using Microsoft.AspNetCore.Identity;
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
        private readonly JwtConfig _options;
        private readonly UserManager<AppUser> _userManager;
        public TokenGenerator(UserManager<AppUser> userManager, IOptionsMonitor<JwtConfig> options)
        {
            _userManager = userManager;
            _options = options.CurrentValue;
        }

        public async Task<string> GenerateToken(AppUser user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Secret);
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
    }
}