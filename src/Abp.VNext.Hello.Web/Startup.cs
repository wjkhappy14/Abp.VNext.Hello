using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Abp.VNext.Hello.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<HelloWebModule>();
            services.AddResponseCompression();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IFeatureCollection features = app.ServerFeatures;
            app.UseResponseCompression();//必须在压缩响应的任何中间件前调用
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
