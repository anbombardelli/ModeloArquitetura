
using Arquitetura.Domain.Entities;

namespace Arquitetura.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        public User Get(User user);
        public bool Update(User user);
        public User Add(User user);
    }
}
