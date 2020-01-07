using Arquitetura.Domain.Entities;
using System.Collections.Generic;

namespace Arquitetura.Domain.Interfaces.Repository
{
    public interface IRepository <TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Delete(int id);

        TEntity Select(int id);

        IList<TEntity> SelectAll();
    }
}
