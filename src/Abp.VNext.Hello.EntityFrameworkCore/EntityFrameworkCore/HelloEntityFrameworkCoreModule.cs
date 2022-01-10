using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
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
        typeof(BloggingEntityFrameworkCoreModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]
    public class HelloEntityFrameworkCoreModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            HelloEfCoreEntityExtensionMappings.Configure();
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<HelloDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);

                options.Entity<Country>(opt =>
                {
                    opt.DefaultWithDetailsFunc = q => q.Include(p => p.StateProvinces);
                });

                options.Entity<StateProvince>(opt =>
                {
                    opt.DefaultWithDetailsFunc = q => q.Include(p => p.Cities);
                });

                options.Entity<Contact>(opt =>
                {
                    opt.DefaultWithDetailsFunc = q => q.Include(p => p.Address);
                });

            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL<HelloDbContext>(x =>
                {
                    
                });
            });
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL<BackgroundJobsDbContext>(x =>
                {

                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL<FeatureManagementDbContext>(x =>
                {

                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL<AbpAuditLoggingDbContext>(x =>
                {
                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL<TenantManagementDbContext>(x =>
                {

                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL<SettingManagementDbContext>(x =>
                {

                });
            });

            Configure<AbpDbContextOptions>((options) =>
            {
                options.UseMySQL<IdentityDbContext>(x =>
                {
                    // x.CommandTimeout(6_000);
                });
            });

            Configure<AbpDbContextOptions>((options) =>
            {
                options.UseMySQL<PermissionManagementDbContext>(x =>
                {
                    //x.CommandTimeout(6_000);
                });
            });

            Configure<AbpDbContextOptions>((options) =>
            {
                options.UseMySQL<IdentityServerDbContext>(x =>
                {
                    // x.MaxBatchSize(4096);
                    // x.CommandTimeout(6_000);
                });
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseMySQL<BloggingDbContext>();
            });
        }
    }
}
