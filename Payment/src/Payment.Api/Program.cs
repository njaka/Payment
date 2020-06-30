using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Payment.Domain.Events;

namespace Payment.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost build = CreateHostBuilder(args).Build();
            DomainEvents.Init(build.Services);
            build.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
