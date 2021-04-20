using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Brain.Cli.Services;

namespace Brain.Cli
{
    public class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<Authentication>();
            services.AddTransient<HttpClient>();
            services.AddTransient<EntryPoint>();
            return services;
            
        }
    }
}