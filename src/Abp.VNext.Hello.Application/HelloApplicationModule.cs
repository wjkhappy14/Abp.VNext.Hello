using Abp.VNext.Hello.XNetty.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Data;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Timing;
using Volo.Abp.Uow;
using Volo.Blogging;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(DotNettyModule),
        typeof(HelloDomainModule),
        typeof(BloggingApplicationModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpBackgroundJobsAbstractionsModule),
        typeof(HelloApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(AbpSettingManagementApplicationModule),
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
            context.Services.TryAddSingleton<IHttpContextAccessor>();
            IConfiguration config =context.Services.GetConfiguration();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HelloApplicationModule>();
            });

            //Configure<AbpRabbitMqEventBusOptions>(options =>
            //{
            //    options.ClientName = "hello-abp";
            //    options.ExchangeName = "hello-exchange";
            //    options.ConnectionName = "Hello";
            //});

            Configure<AbpBackgroundJobWorkerOptions>(options =>
            {
                options.DefaultTimeout = 3600;
                options.JobPollPeriod = 2;
                options.MaxJobFetchCount = 8;
                options.DefaultFirstWaitDuration = 3600;
                options.DefaultWaitFactor = Math.E;
            });
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
            Configure<AbpUnitOfWorkOptions>((options) =>
            {
                options.IsolationLevel = IsolationLevel.ReadUncommitted;
                options.IsTransactional = false;
            });
            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });

            }
    }
}
