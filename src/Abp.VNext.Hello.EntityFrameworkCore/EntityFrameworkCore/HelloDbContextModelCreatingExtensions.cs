using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users;

namespace Abp.VNext.Hello.EntityFrameworkCore
{
    public static class HelloDbContextModelCreatingExtensions
    {
        public static void ConfigureHello(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            AbpModelBuilderConfigurationOptions options = new AbpModelBuilderConfigurationOptions(DbProperties.DbTablePrefix,DbProperties.DbSchema);
         
        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser: class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...
        }
    }
}