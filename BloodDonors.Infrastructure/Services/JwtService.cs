using System;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BloodDonors.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonors.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public JwtDto CreateToken(string pesel, string role)
        {
            var now = DateTime.UtcNow;
            var epochNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, pesel),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Iat, epochNow.ToString(), ClaimValueTypes.Integer64),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("jwt:key").Value)),
                SecurityAlgorithms.HmacSha256);

            var expires = now.AddMinutes(int.Parse(configuration.GetSection("jwt:expiresIn").Value));

            var jwt = new JwtSecurityToken(
                issuer: configuration.GetSection("jwt:issuer").Value,
                claims: claims,
                signingCredentials: signingCredentials,
                notBefore: now,
                expires: expires
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto()
            {
                Token = token,
                Expires = expires
            };
        }
    }
}