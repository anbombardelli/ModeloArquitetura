using Arquitetura.Data.Repository;
using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Entities.Auth;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Lib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Arquitetura.Services.Services
{
    public class AuthenticationService : IAuthenticateService
    {
        private static User UserAuthentication;
        private static SigningConfiguration SigningConfiguration;
        private static JWTConfiguration TokenConfiguration;
        private IConfiguration _configuration;

        public AuthenticationService
            (
            SigningConfiguration signingConfiguration, 
            JWTConfiguration tokenConfiguration , 
            IConfiguration configuration
            )
        {
            SigningConfiguration = signingConfiguration;
            TokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }

        public object Authenticate(User user)
        {
            bool ValidCredentials = false;
            User userBase = null;

            if (user != null && !String.IsNullOrWhiteSpace(user.Email) && !String.IsNullOrWhiteSpace(user.Password))
            {

                IUserRepository UserRepository = new UserRepository(_configuration);

                userBase = UserRepository.Get(new User { Email = user.Email });

     
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
   
        private static Object CreateToken(User userBase)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(Convert.ToString(userBase.Id), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userBase.FirstName),
                    }
                );

            DateTime CreatedDate = DateTime.Now;
            DateTime ExpirationDate = CreatedDate + TimeSpan.FromSeconds(TokenConfiguration.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenConfiguration.Issuer,
                Audience = TokenConfiguration.Audience,
                SigningCredentials = SigningConfiguration.SigningCredentials,
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
