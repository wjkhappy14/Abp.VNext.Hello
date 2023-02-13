using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.VNext.Hello.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(HelloEntityFrameworkCoreDbMigrationsModule),
        typeof(HelloApplicationContractsModule)
        )]
    public class HelloDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
            context.Services.Configure<AbpDbContextOptions>((o) =>
            {
                o.UseSqlServer((s) => { });
            });
        }
    }
}
