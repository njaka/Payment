namespace Payment.Api
{
    using Microsoft.AspNetCore.Builder;
    using Payment.Api.Configuration;
    using Prometheus;

    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures swagger Middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="swaggerConfiguration">The swagger configuration.</param>
        /// <returns></returns>
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, SwaggerConfigurationModel swaggerConfiguration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                foreach (var current in swaggerConfiguration.Versions)
                {
                    c.SwaggerEndpoint($"/swagger/v{current.Version}/swagger.json", current.Title);
                }
                c.RoutePrefix = "";
            });

            return app;
        }
    }
}
