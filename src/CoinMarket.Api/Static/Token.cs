using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoinMarker.Infrastructure.Model;
using Microsoft.IdentityModel.Tokens;

namespace CoinMarket.Api.Static
{
    public static class Token
    {
        public static string GenerateJwtToken(UserModel user, string secretKey)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);
            
            var JWToken = new JwtSecurityToken(
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddHours(7200)).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            // token = HttpUtility.UrlEncode(StringCompressor.CompressString(token));
            return token;
        } 
        private static IEnumerable<Claim> GetUserClaims(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id",user.Id.ToString())
            };
            return claims;
        }
    }
}