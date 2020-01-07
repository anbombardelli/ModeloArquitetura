using Arquitetura.Domain.Interfaces.Services;
using Arquitetura.Services.Services;
using Arquitetura.Services.Validator.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Arquitetura.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureDependenciesService(this IServiceCollection services)
        {
            services.AddScoped<INotification, Notification>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IAuthenticateService, AuthenticationService>();

            return services;
        }
    }
}
