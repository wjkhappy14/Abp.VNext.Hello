using EasyAbp.Abp.SettingUi;
using EasyAbp.EShop.Stores;
using EasyAbp.PrivateMessaging;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;
using Volo.Blogging;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(HelloApplicationContractsModule),
        typeof(PrivateMessagingHttpApiModule),
        typeof(BloggingHttpApiModule),
        typeof(SettingUiHttpApiModule),
        typeof(EShopStoresHttpApiModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule)
        )]
    public class HelloHttpApiModule : AbpModule
    {

    }
}
