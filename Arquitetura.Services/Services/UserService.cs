using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Validator;
using Arquitetura.Services.Validator.Notification;
using FluentValidation;
using System.Collections.Generic;

namespace Arquitetura.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(INotification notification, IUserRepository repository) : base(notification)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public User Get(int id)
        {
            return _repository.Select(id);
        }

        public IList<User> Get()
        {
            return _repository.SelectAll();
        }

        public User Post(User user)
        {
            Validation(user);

            _repository.Insert(user);
            return user;
        }

        public User Put(User user)
        {
            Validation(user);

            _repository.Update(user);
            return user;
        }

        private void Validation(User user)
        {
            var validator = new UserValidator();
            if (!Validate(validator, user))
                validator.ValidateAndThrow(user);
        }        
    }
}
