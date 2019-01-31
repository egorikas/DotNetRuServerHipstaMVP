using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetRuServerHipstaMVP.Api.Application.Options;
using DotNetRuServerHipstaMVP.Domain.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DotNetRuServerHipstaMVP.Api.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthService(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions;
        }

        public Task<AuthToken> Generate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new NotFoundException("Неправильный email или пароль");
            if (email != "dotnetru@dotnetru.com" && password != "dotnetru@dotnetru")
                throw new NotFoundException("Неправильный email или пароль");

            var authToken = GenerateToken();

            var token = new AuthToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(authToken),
            };

            return Task.FromResult(token);
        }

        public Task<AuthToken> Generate(int userId)
        {
            throw new NotImplementedException();
        }

        public Task CheckPassword(int userId, string oldPassword)
        {
            throw new NotImplementedException();
        }

        private JwtSecurityToken GenerateToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "dotnetru@dotnetru.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


            var key = _authOptions.Value.GetSymmetricSecurityKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var authToken = new JwtSecurityToken(
                _authOptions.Value.Issuer,
                _authOptions.Value.Audience,
                claims,
                signingCredentials: creds
            );

            return authToken;
        }
    }
}