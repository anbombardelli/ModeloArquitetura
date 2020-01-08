using Arquitetura.Data.Repository;
using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Entities.Auth;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Lib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Arquitetura.Services.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private static SigningConfiguration _signingConfiguration;
        private static JWTConfiguration _tokenConfiguration;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
            SigningConfiguration signingConfiguration,
            JWTConfiguration tokenConfiguration,
            IConfiguration configuration)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }

        public object Authenticate(User user)
        {
            bool ValidCredentials = false;
            User userBase = null;

            if (user != null && !string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password))
            {
                IUserRepository userRepository = new UserRepository(_configuration);
                userBase = userRepository.Get(new User { Email = user.Email });

                if (userBase == null)
                    throw new Exception("User not found!");

                ValidCredentials = PasswordExtension.VerifyPasswordHash(user.Password, userBase.PasswordHash, userBase.PasswordSalt);
            }

            if (ValidCredentials)
                return CreateToken(userBase);
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Authentication failed"
                };
            }
        }

        private static object CreateToken(User userBase)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(Convert.ToString(userBase.Id), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userBase.FirstName),
                    }
                );

            DateTime CreatedDate = DateTime.Now;
            DateTime ExpirationDate = CreatedDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = CreatedDate,
                Expires = ExpirationDate
            });
            var token = handler.WriteToken(securityToken);

            return new
            {
                authenticated = true,
                created = CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = ExpirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
