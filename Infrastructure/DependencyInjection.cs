using Domain.Interfaces.Repositories;
using Infrastructure.Concrets.Repositories;
using Infrastructure.Context;
using Infrastructure.Health;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EcommerceDb");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("String de conexao faltando");

        services.AddDbContext<EcommerceContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        ConfigureHealthCheck(services);
        return services;
    }

    private static void ConfigureHealthCheck(this IServiceCollection servicers)
    {
        servicers.AddHealthChecks()
                 .AddCheck<DataBaseEcommerceHealthCheck>("EcommerceDb", HealthStatus.Unhealthy);
    }
}