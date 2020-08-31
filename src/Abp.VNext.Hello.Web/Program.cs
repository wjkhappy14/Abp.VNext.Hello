using System;
using System.Threading.Tasks;
using Abp.VNext.Hello.XNetty.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Abp.VNext.Hello.Web
{
    public class Program
    {
        static string url = "http://0.0.0.0/";
        public static async Task<int> Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()

                .WriteTo.Logger(lc => lc
                .WriteTo.Async(c => c.File("Logs/logs.txt")))

                .CreateLogger();
            try
            {
                Log.Information("Starting web host.");
                Console.WriteLine($"Http Host Running on {url} {Environment.NewLine}TCP(Netty) Running on Port:443");
                await XServerBootstrap.RunServerAsync(443);
                await CreateHostBuilder(args)
                    .Build()
                    .RunAsync();

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
            .ConfigureServices((context, services) =>
            {
                services.Configure<KestrelServerOptions>(context.Configuration.GetSection("Kestrel"));
            })
            .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.PreferHostingUrls(true);
                    webHostBuilder.UseStaticWebAssets();

                    webHostBuilder.UseUrls(url);
                    //Kestrel https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.1
                    webHostBuilder.ConfigureKestrel(serverOptions =>
                     {
                         //为整个应用设置并发打开的最大 TCP 连接数：
                         serverOptions.Limits.MaxConcurrentConnections = 100;
                         serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
                         serverOptions.Limits.MaxRequestBodySize = 10 * 1024;

                         serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                         serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);

                     });
                    webHostBuilder.UseStartup<Startup>();

                    webHostBuilder.UseKestrel(opts =>
                    {
                        //https://andrewlock.net/5-ways-to-set-the-urls-for-an-aspnetcore-app/
                        //opts.ListenAnyIP(8080, x =>
                        //{

                        //});
                    });
                })
                .UseAutofac()
                .UseSerilog();
    }
}
