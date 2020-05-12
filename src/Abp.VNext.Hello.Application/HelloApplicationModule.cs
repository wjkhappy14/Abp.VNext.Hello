using EasyAbp.PrivateMessaging;
using System;
using System.Data;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(HelloDomainModule),
        typeof(PrivateMessagingApplicationModule),
        typeof(AbpAccountApplicationModule),
        typeof(HelloApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule)
        )]
    public class HelloApplicationModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            base.PreConfigureServices(context);
        }


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

            Configure<AbpBackgroundJobWorkerOptions>(options =>
            {
                options.DefaultTimeout = 3600;
                options.JobPollPeriod = 2;
                options.MaxJobFetchCount = 8;
                options.DefaultFirstWaitDuration = 3600;
                options.DefaultWaitFactor = Math.E;
            });
            Configure<AbpBackgroundWorkerOptions>(options =>
            {
                options.IsEnabled = true;
            });
            Configure<AbpUnitOfWorkOptions>((options) =>
            {
                options.IsolationLevel = IsolationLevel.ReadCommitted;
                options.IsTransactional = true;
            });
            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });
        }
    }
}
