using Arquitetura.Domain.Entities;
using System.Collections.Generic;

namespace Arquitetura.Domain.Interfaces.Services
{
    public interface IUserService
    {
        bool Post(User user);

        bool Put(User user);

        void Delete(int id);

        User Get(int id);

        IList<User> Get();
    }
}
