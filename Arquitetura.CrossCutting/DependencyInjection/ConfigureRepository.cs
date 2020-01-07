using Arquitetura.Data.Repository;
using Arquitetura.Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Arquitetura.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static IServiceCollection ConfigureDependenciesRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
