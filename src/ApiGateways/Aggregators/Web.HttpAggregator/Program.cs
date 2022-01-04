using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Web.HttpAggregator.HttpAggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostBuilder host = CreateHostBuilder(args);
            IHost build = host.Build();
            build.Run();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               })
               .UseAutofac()
               .UseSerilog();

    }
}
