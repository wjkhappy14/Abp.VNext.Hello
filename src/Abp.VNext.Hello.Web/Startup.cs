using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            Console.ForegroundColor = ConsoleColor.Green;
            string info = $"Web 服务 {env.ApplicationName} {Environment.NewLine}运行于 {env.ContentRootPath}{Environment.NewLine} 请不要关闭！";
            Console.WriteLine(info);
            app.InitializeApplication();
        }
    }
}
