using Arquitetura.Domain.Entities;
using FluentValidation;
using System.Collections.Generic;

namespace Arquitetura.Domain.Interfaces.Services
{
    public interface IService<T> where T : BaseEntity
    {
        T Post<V>(T obj) where V : AbstractValidator<T>;

        T Put<V>(T obj) where V : AbstractValidator<T>;

        void Delete(int id);

        T Get(int id);

        IList<T> Get();
    }
}
