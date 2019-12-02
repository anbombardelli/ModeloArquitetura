using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Arquitetura.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
        }
    }
}
