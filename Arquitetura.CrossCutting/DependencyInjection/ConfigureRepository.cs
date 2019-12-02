using Arquitetura.Data.Repository;
using Arquitetura.Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Arquitetura.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
