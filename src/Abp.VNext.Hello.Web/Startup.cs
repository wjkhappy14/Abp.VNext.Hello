using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Abp.VNext.Hello.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<HelloWebModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IFeatureCollection features = app.ServerFeatures;

           // app.Run((context) =>
           // {
           //     string url = context.Request.GetDisplayUrl();
           //     System.Diagnostics.Debug.WriteLine(url);
           //     return Task.CompletedTask;
           // });
            Console.ForegroundColor = ConsoleColor.Green;
            string info = $"Web 服务 {env.ApplicationName} {Environment.NewLine}运行于 {env.ContentRootPath}{Environment.NewLine} 请不要关闭！";
            Console.WriteLine(info);
            app.InitializeApplication();
        }
    }
}
