using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Abp.VNext.Hello.EntityFrameworkCore
{
    [DependsOn(
        typeof(HelloEntityFrameworkCoreModule)
        )]
    public class HelloEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HelloMigrationsDbContext>();
        }
    }
}
