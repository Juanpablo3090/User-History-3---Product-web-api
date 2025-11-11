
using Microsoft.Extensions.DependencyInjection;
using webProductos.Application.Services;

namespace webProductos.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
