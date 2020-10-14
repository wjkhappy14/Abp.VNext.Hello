using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Abp.VNext.Hello.EntityFrameworkCore
{
    public static class HelloDbContextModelCreatingExtensions
    {
        public static void ConfigureHello(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(HelloConsts.DbTablePrefix + "YourEntities", HelloConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}