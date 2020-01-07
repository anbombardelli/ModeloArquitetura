using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Lib.Extensions;
using System;

namespace Arquitetura.Services.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _userRepository;

        public RegisterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(User user)
        {
            try
            {
                var UserData = _userRepository.Get(new User { Email = user.Email });

                if (UserData != null)
                    throw new Exception("O email informado não está disponível.");

                byte[] passwordHash, passwordSalt;
                PasswordExtension.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _userRepository.Insert(user);

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

            user = _userRepository.Get(new User { Email = user.Email });

            if (user == null)
                throw new Exception("User not found");

            byte[] passwordHash, passwordSalt;

            PasswordExtension.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Update(user);

            return user;
        }
    }
}
