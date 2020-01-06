using Arquitetura.Domain.Entities;
using Arquitetura.Services.Validator.Notification;
using FluentValidation;
using FluentValidation.Results;

namespace Arquitetura.Services.Services
{
    public class BaseService
    {
        private readonly INotification _notification;

        public BaseService(INotification notification)
        {
            _notification = notification;
        }

        public void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        public void Notify(string errorMessage)
        {
            _notification.Handle(new Message(errorMessage));
        }

        public bool Validate<TValidation, TEntity>(TValidation validation, TEntity entity)
            where TValidation : AbstractValidator<TEntity> where TEntity : BaseEntity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }
    }
}
