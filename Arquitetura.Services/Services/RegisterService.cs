using Arquitetura.Data.Repository;
using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Lib.Extensions;
using Microsoft.Extensions.Configuration;
using System;

namespace Arquitetura.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private IConfiguration _configuration;

        public RegisterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public User Create(User user)
        {
            try
            {
                IUserRepository repository = new UserRepository(_configuration);

                var UserData = repository.Get(new User { Email = user.Email });


                if (UserData != null)
                    throw new Exception("O email informado não está disponível.");

                byte[] passwordHash, passwordSalt;
                PasswordExtension.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                repository.Add(user);

                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User PasswordUpdate(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return null;

            IUserRepository UserRepository = new UserRepository(_configuration);

            user = UserRepository.Get(new User { Email = user.Email });

            if (user == null)
                throw new Exception("User not found");

            byte[] passwordHash, passwordSalt;

            PasswordExtension.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            UserRepository.Update(user);

            return user;
        }
    }
}
