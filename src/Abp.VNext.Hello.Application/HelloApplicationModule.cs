using System;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Timing;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(HelloDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(HelloApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule)
        )]
    public class HelloApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HelloApplicationModule>();
            });

            Configure<AbpRabbitMqEventBusOptions>(options =>
            {
                options.ClientName = "hello-abp";
                options.ExchangeName = "hello-exchange";
                options.ConnectionName = "Hello";
            });
            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });
        }
    }
}
