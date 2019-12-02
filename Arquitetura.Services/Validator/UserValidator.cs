using Arquitetura.Domain.Entities;
using FluentValidation;
using System;

namespace Arquitetura.Services.Validator
{
    public class UserValidator : AbstractValidator<UserEntity>
    {
        public UserValidator()
        {
            RuleFor(c => c)
                    .NotNull()
                    .OnAnyFailure(x =>
                    {
                        throw new ArgumentNullException("Can't found the object.");
                    });


            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Is necessary to inform the name.");
        }
    }
}
