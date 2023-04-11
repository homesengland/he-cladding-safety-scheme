using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.Middleware
{
    public class ReadyHealthCheck : IHealthCheck
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<ReadyHealthCheck> _logger;

        public ReadyHealthCheck(IDbConnection dbConnection, ILogger<ReadyHealthCheck> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                if (_dbConnection is DbConnection dbConn)
                {
                    await dbConn.OpenAsync(cancellationToken);
                    return HealthCheckResult.Healthy();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Ready health check failed - unable to connect to DB");
                return HealthCheckResult.Unhealthy();
            }

            _logger.LogCritical("Unable to determine DB connection type for ready health check");
            return HealthCheckResult.Unhealthy();
        }
    }
}
