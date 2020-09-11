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

            AbpModelBuilderConfigurationOptions options = new AbpModelBuilderConfigurationOptions(
             DbProperties.DbTablePrefix,
             DbProperties.DbSchema
         );


            builder.Entity<City>(b =>
            {
                b.ToTable(options.TablePrefix + "Cities");
                b.HasOne<StateProvince>().WithMany().HasForeignKey(ur => ur.StateProvinceId);
                b.HasIndex(q => q.Id);
            });

            builder.Entity<StateProvince>(b =>
            {
                b.ToTable(options.TablePrefix + "StateProvinces");
                b.HasOne<Country>().WithMany().HasForeignKey(ur => ur.CountryId);
                b.HasMany(u => u.Cities).WithOne().HasForeignKey(uc => uc.StateProvinceId);
                b.HasIndex(q => q.Id);
            });

            builder.Entity<Country>(b =>
            {
                b.ToTable(options.TablePrefix + "Countries");
                b.HasMany(u => u.StateProvinces).WithOne().HasForeignKey(uc => uc.CountryId);
                b.HasIndex(q => q.Id);
            });
        }

        public static void ConfigureCustomUserProperties<TUser>(this EntityTypeBuilder<TUser> b)
            where TUser: class, IUser
        {
            //b.Property<string>(nameof(AppUser.MyProperty))...
        }
    }
}