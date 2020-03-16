using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Abp.VNext.Hello
{
    public class HelloWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<HelloWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}