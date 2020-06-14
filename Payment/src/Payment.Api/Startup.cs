using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payment.Api.Configuration;

namespace Payment.Api
{
    public class Startup
    {

        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks();
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddPaymentApplication();
            services.AddPaymentPresenterV1();
            services.AddApiVersioning(Configuration.GetVersioningConfiguration());
            services.AddSwagger(Configuration.GetSwaggerConfiguration());
            services.AddHttpExceptionFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
            }

            appBuilder.UseHttpsRedirection();
            appBuilder.UseStaticFiles();
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("health");
            });
            appBuilder.ConfigureSwagger(Configuration.GetSwaggerConfiguration());
        }
    }
}
