using Abp.VNext.Hello.XNetty.Server;
using EasyAbp.Abp.EventBus.Cap;
using EasyAbp.Abp.SettingUi;
using EasyAbp.Abp.WeChat.MiniProgram;
using EasyAbp.PrivateMessaging;
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
using Volo.Abp.Timing;
using Volo.Abp.Uow;
using Volo.Blogging;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(DotNettyModule),
        typeof(HelloDomainModule),
        typeof(PrivateMessagingApplicationModule),
        typeof(BloggingApplicationModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpSettingUiApplicationModule),
        typeof(AbpBackgroundJobsAbstractionsModule),
        typeof(HelloApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpAspNetCoreSignalRModule),
       // typeof(AbpWeChatMiniProgramModule),
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

            context.AddCapEventBus(capOptions =>
            {
                capOptions.ProducerThreadCount = Environment.ProcessorCount;
                capOptions.ConsumerThreadCount = Environment.ProcessorCount;
                capOptions.DefaultGroupName = "Abp.VNext.Hello.Cap-Queue";
                capOptions.TopicNamePrefix = "Abp.VNext.Hello";
                capOptions.FailedThresholdCallback = (failed) =>
                {
                    switch (failed.MessageType)
                    {
                        case DotNetCore.CAP.Messages.MessageType.Publish:
                            System.Diagnostics.Debug.WriteLine(failed.Message);
                            break;
                        case DotNetCore.CAP.Messages.MessageType.Subscribe:
                            System.Diagnostics.Debug.WriteLine(failed.Message);
                            break;
                        default:
                            break;
                    }
                };
                capOptions.UseSqlServer(config["ConnectionStrings:Default"]);
                capOptions.UseRabbitMQ(x =>
                {
                    x.HostName = "117.50.40.186";
                    x.UserName = "guest";
                    x.Password = "guest";
                    x.VirtualHost = "/";
                });// 服务器地址配置，支持配置IP地址和密码
                capOptions.UseDashboard(dashboard => {
                    //DotNetCore.CAP.Dashboard.LocalRequestsOnlyAuthorizationFilter
                    dashboard.StatsPollingInterval = 600;
                });
            });
        }
    }
}
