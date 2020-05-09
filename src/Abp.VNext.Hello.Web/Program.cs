using System;
using System.Threading.Tasks;
using Abp.VNext.Hello.XNetty.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Abp.VNext.Hello.Web
{
    public class Program
    {
        static string url = "http://0.0.0.0:8080/";
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .CreateLogger();

            try
            {
                Log.Information("Starting web host.");
                Console.WriteLine($"Http Host Running on {url} {Environment.NewLine}TCP(Netty) Running on Port:666");
                await CreateHostBuilder(args).Build().RunAsync();
                await XServerBootstrap.RunServerAsync(666);
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.PreferHostingUrls(true);
                    webHostBuilder.UseStaticWebAssets();
                    webHostBuilder.UseUrls(url);
                    webHostBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}
