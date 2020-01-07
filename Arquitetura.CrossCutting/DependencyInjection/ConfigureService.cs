using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Services;
using Arquitetura.Services.Validator.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Arquitetura.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureDependenciesService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotification, Notification>();
            serviceCollection.AddScoped<IUserService, UserService>();

            return serviceCollection;
        }
    }
}
