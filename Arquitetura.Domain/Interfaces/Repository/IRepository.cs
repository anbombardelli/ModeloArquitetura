using Arquitetura.Domain.Entities;
using System.Collections.Generic;

namespace Arquitetura.Domain.Interfaces.Repository
{
    public interface IRepository <T> where T : BaseEntity
    {
        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

        T Select(int id);

        IList<T> SelectAll();
    }
}
