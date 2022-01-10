using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Abp.VNext.Hello.EntityFrameworkCore
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
    /// </summary>
    [ConnectionStringName("HelloDB")]
    public class HelloDbContext : AbpDbContext<HelloDbContext>
    {

        public DbSet<City> Cities { get; set; }

        public DbSet<EztvItem> Eztvs { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<StateProvince> StateProvinces { get; set; }

        public DbSet<Scheduler> Schedulers { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public HelloDbContext(DbContextOptions<HelloDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            builder.ConfigureHello();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableDetailedErrors(true);
            optionsBuilder.ConfigureWarnings(warnings =>
            {

            });
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
