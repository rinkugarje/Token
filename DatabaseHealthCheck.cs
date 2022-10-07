using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LoginTokenTask
{
    public static class DatabaseHealthCheck
    {
        public static HealthCheckResult Check(string connectionString)
        {
            //code to check if db is running
            return HealthCheckResult.Healthy();
            //return HealthCheckResult.Unhealthy();
        }
    }
}
