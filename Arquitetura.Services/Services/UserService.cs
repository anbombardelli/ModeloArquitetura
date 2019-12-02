using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Domain.Interfaces.Services;

namespace Arquitetura.Services.Services
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        public UserService(IRepository<UserEntity> repository) : base(repository)
        {

        }
    }
}
