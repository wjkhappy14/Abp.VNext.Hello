using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello.EntityFrameworkCore
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/ef/core/dbcontext-configuration/#avoiding-dbcontext-threading-issues
    /// </summary>
    [ConnectionStringName("Awesome")]
    public class HelloDbContext : AbpDbContext<HelloDbContext>
    {

    

        public HelloDbContext(DbContextOptions<HelloDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
