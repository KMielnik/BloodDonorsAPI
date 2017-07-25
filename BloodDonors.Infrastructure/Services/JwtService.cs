using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public string CreateToken(string pesel, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, pesel),
                new Claim(ClaimTypes.Role, role)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("jwt:key").Value)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: configuration.GetSection("jwt:issuer").Value,
                claims: claims,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}