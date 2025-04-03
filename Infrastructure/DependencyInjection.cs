using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EcommerceDb");

        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("String de conexao faltando");

        services.AddDbContext<EcommerceContext>(opt => opt.UseNpgsql(connectionString));

        return services;
    }
}