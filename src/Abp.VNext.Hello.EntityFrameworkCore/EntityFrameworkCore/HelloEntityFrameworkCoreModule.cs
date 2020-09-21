using EasyAbp.EShop.EntityFrameworkCore;
using EasyAbp.PrivateMessaging.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Blogging.EntityFrameworkCore;

namespace Abp.VNext.Hello.EntityFrameworkCore
{
    [DependsOn(
        typeof(HelloDomainModule),
        typeof(AbpDapperModule),
        typeof(EShopEntityFrameworkCoreModule),
        typeof(PrivateMessagingEntityFrameworkCoreModule),
        typeof(BloggingEntityFrameworkCoreModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqliteModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]
    public class HelloEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HelloDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<HelloDbContext>(x =>
                {

                });
            });
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<BackgroundJobsDbContext>(x =>
                {

                });
            });



            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<AbpAuditLoggingDbContext>(x =>
                {
                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<TenantManagementDbContext>(x =>
                {

                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<SettingManagementDbContext>(x =>
                {

                });
            });
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite<PrivateMessagingDbContext>(x =>
                {

                });
            });

            Configure<AbpDbContextOptions>((options) =>
            {
                options.UseSqlServer<IdentityDbContext>(x =>
                {
                    x.CommandTimeout(6_000);
                });
            });

            Configure<AbpDbContextOptions>((options) =>
            {
                options.UseSqlServer<PermissionManagementDbContext>(x =>
                {
                    x.CommandTimeout(6_000);
                });
            });

            Configure<AbpDbContextOptions>((options) =>
            {
                options.UseSqlServer<IdentityServerDbContext>(x =>
                {
                    x.MaxBatchSize(4096);
                    x.CommandTimeout(6_000);
                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer<BloggingDbContext>();
            });
        }
    }
}
