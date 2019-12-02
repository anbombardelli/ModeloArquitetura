using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;

namespace Arquitetura.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
