using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System.Diagnostics;

namespace Abp.VNext.Hello
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //ShowPII
            IdentityModelEventSource.ShowPII = true;
            services.AddApplication<HelloHttpApiHostModule>((cnf) =>
            {
                Debug.Write(cnf.Configuration);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}
