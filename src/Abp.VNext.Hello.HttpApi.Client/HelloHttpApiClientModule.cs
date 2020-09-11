using EasyAbp.Abp.SettingUi;
using EasyAbp.EShop;
using EasyAbp.PrivateMessaging;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Blogging;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(HelloApplicationContractsModule),
        typeof(SettingUiHttpApiClientModule),
        typeof(EShopHttpApiClientModule),
        typeof(PrivateMessagingHttpApiClientModule),
        typeof(BloggingHttpApiClientModule),
        typeof(AbpAccountHttpApiClientModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(AbpPermissionManagementHttpApiClientModule),
        typeof(AbpTenantManagementHttpApiClientModule),
        typeof(AbpFeatureManagementHttpApiClientModule)
    )]
    public class HelloHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(HelloApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
