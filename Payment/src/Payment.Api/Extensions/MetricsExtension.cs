using Microsoft.AspNetCore.Builder;
using Prometheus;

namespace Payment.Api.Extensions
{
    /// <summary>
    /// Metric Extension
    /// </summary>
    public static class MetricsExtension
    {
        /// <summary>
        /// Use Custom Http Metricss
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomHttpMetrics(this IApplicationBuilder appBuilder)
        {
            return appBuilder
                .UseMetricServer()
                .UseHttpMetrics(options =>
                {
                    options.RequestDuration.Enabled = false;
                    options.InProgress.Enabled = false;
                    options.RequestCount.Counter = Metrics.CreateCounter(
                        "payment_gateway_request_total",
                        "HTTP Requests Total",
                        new CounterConfiguration { LabelNames = new[] { "method", "controller", "action", "code" } });
                });
        }
    }
}
