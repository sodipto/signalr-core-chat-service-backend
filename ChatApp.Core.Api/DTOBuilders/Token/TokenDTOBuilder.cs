using ChatApp.Core.Api;
using ChatApp.Core.Domain;
using ChatApp.Core.Utils.DTOs;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using static System.Net.WebRequestMethods;

namespace ChatApp.Core.DTOBuilders
{
    public static class TokenDTOBuilder
    {
        public static TokenDTO GetTokenDTO(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalConfig.GetConfiguration("JWT:Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expiredIn = DateTime.Now.AddYears(1);

            var claims = new List<Claim>
            {
                new Claim("UserID", user.ID.ToString()),
                new Claim("Email", user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: GlobalConfig.GetConfiguration("JWT:Issuer"),
                claims: claims,
                expires: expiredIn,
                signingCredentials: credentials
              );

            var dto = new TokenDTO()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiredIn = new DateTimeOffset(expiredIn).ToUnixTimeMilliseconds()
            };

            return dto;
        }
    }
}
