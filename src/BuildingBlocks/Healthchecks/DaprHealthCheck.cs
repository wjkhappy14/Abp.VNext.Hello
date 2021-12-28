using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Abp.VNext.Hello.BuildingBlocks.Healthchecks
{
    public class DaprHealthCheck : IHealthCheck
    {
        private readonly DaprClient _daprClient;

        public DaprHealthCheck(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var healthy = await _daprClient.CheckHealthAsync(cancellationToken);
            
            if (healthy)
            {
                return HealthCheckResult.Healthy("Dapr sidecar健康着呢.");
            }

            return new HealthCheckResult(
                context.Registration.FailureStatus,
                "Dapr sidecar 有点不舒服.");
        }
    }
}
