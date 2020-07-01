using Microsoft.Extensions.Configuration;
using Payment.Service.Grpc.Configuration.Model;

namespace Payment.Service.Grpc.Configuration
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Gets the versioning configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static EventSourcingConfigurationModel GetEventSourcingConfigurarionModel(this IConfiguration configuration)
        {
            return configuration.GetSection("EventSourcing").Get<EventSourcingConfigurationModel>();
        }
    }
}
