namespace Payment.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Payment.Api.Configuration;
    using Payment.Api.Configuration.Versioning;
    using Payment.Api.Filter;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;


    public static class ServiceCollectionExtension
    {
        internal static IServiceCollection AddHttpExceptionFilter(this IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(typeof(HttpExceptionFilter)); });

            return services;
        }

        internal static IServiceCollection AddSwagger(this IServiceCollection services, SwaggerConfigurationModel swaggerConfiguration)
        {

            services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(y => y.FullName);
                foreach (VersionConfigurationModel current in swaggerConfiguration.Versions)
                {
                    x.SwaggerDoc($"v{current.Version}", new OpenApiInfo
                    {
                        Version = $"v{current.Version}",
                        Title = current.Title,
                        Description = current.Description,
                        Contact = new OpenApiContact
                        {
                            Email = current.Email
                        }
                    });
                }

                x.DocInclusionPredicate((version, apiDescriptor) =>
                {
                    if (!apiDescriptor.TryGetMethodInfo(out MethodInfo mi))
                    {
                        return false;
                    }

                    if (!Regex.IsMatch(apiDescriptor.RelativePath, @"v{version}")
                    && !Regex.IsMatch(apiDescriptor.RelativePath, @"v(\d+\.)?(\d+\.)?(\*|\d+)"))
                    {
                        return false;
                    }

                    System.Collections.Generic.IEnumerable<ApiVersion> versions = mi.DeclaringType.GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);

                    ApiVersion[] maps = mi.GetCustomAttributes()
                    .OfType<MapToApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions).ToArray();

                    return versions.Any(v => $"v{v.ToString()}" == version)
                    && (maps.Length == 0 || maps.Any(v => $"v{v.ToString()}" == version));
                });

                System.Collections.Generic.IEnumerable<string> documentations = swaggerConfiguration.XmlDocumentation ?? new[] { $"{Assembly.GetEntryAssembly().GetName().Name}.xml" };

                foreach (string documentationPath in documentations)
                {
                    string xmlPath = Path.Combine(AppContext.BaseDirectory, documentationPath);
                    x.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }
        public static IServiceCollection AddApiVersioning(this IServiceCollection services, VersioningConfigurationModel versioningConfiguration)
        {
            services.AddApiVersioning(opts =>
            {
                opts.DefaultApiVersion = ApiVersion.Parse(versioningConfiguration.Default);
                opts.AssumeDefaultVersionWhenUnspecified = true;
                opts.ApiVersionReader = new UrlSegmentApiVersionReader();
                opts.RouteConstraintName = versioningConfiguration.RouteConstraintName;
            });

            return services;
        }

    }
}
