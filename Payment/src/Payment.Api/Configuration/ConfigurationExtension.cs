using Microsoft.Extensions.Configuration;
using Payment.Api.Configuration.Model;
using Payment.Api.Configuration.Versioning;

namespace Payment.Api.Configuration
{
    public static class ConfigurationExtension
    {
        /// <summary>
        /// Gets the versioning configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static VersioningConfigurationModel GetVersioningConfiguration(this IConfiguration configuration)
        {
            return configuration.GetSection("Api:Versioning").Get<VersioningConfigurationModel>();
        }

        /// <summary>
        /// Gets the swagger configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static SwaggerConfigurationModel GetSwaggerConfiguration(this IConfiguration configuration)
        {
            var model = configuration.GetSection("Api:Swagger").Get<SwaggerConfigurationModel>();

            return model;
        }

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
