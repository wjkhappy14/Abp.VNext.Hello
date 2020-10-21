using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Abp.VNext.Hello
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //ShowPII
            IdentityModelEventSource.ShowPII = true;
            services.AddApplication<HelloIdentityServerModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
