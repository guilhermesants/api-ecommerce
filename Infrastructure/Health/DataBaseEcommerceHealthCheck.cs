using Infrastructure.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure.Health;

internal class DataBaseEcommerceHealthCheck : IHealthCheck
{
    private readonly EcommerceContext _context;

    public DataBaseEcommerceHealthCheck(EcommerceContext context)
       => _context = context;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
            return canConnect ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Erro ao verificar o banco de dados.", ex);
        }
    }
}
