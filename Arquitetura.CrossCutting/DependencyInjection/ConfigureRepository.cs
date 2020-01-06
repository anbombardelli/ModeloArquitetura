using Arquitetura.Data.Repository;
using Arquitetura.Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Arquitetura.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static IServiceCollection ConfigureDependenciesRepository(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            return serviceCollection;
        }
    }
}
