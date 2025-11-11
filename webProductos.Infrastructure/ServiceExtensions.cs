
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webProductos.Infrastructure.Persistence;

namespace webProductos.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("DefaultConnection") ?? config["ConnectionStrings:DefaultConnection"];
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseMySql(conn, ServerVersion.AutoDetect(conn))
        );
        return services;
    }
}
