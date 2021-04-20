using System;
using Microsoft.Extensions.DependencyInjection;

namespace Brain.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var entry = serviceProvider.GetService<EntryPoint>();
            
            if (entry is null)
            {
                Console.WriteLine("Something went wrong!");
                return;
            }
            
            entry.Run(args);
        }
    }
}
