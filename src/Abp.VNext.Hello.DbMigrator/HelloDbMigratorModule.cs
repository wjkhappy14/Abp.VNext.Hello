using Abp.VNext.Hello.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Abp.VNext.Hello.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(HelloEntityFrameworkCoreModule),
        typeof(HelloApplicationContractsModule)
        )]
    public class HelloDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
