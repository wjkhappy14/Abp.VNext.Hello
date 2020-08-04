using Abp.VNext.Hello.Localization;
using EasyAbp.Abp.SettingUi;
using EasyAbp.EShop.Stores;
using EasyAbp.PrivateMessaging;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Blogging;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(EShopStoresDomainSharedModule),
        typeof(SettingUiDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(PrivateMessagingDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(BloggingDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpTenantManagementDomainSharedModule)
        )]
    public class HelloDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HelloDomainSharedModule>("Abp.VNext.Hello");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<HelloResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Hello");
            });
        }
    }
}
