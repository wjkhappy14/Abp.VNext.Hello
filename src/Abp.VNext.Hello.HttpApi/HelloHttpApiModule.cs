using EasyAbp.Abp.SettingUi;
using EasyAbp.EShop;
using EasyAbp.PrivateMessaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using System.Text;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;
using Volo.Blogging;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(AbpIdentityServerDomainModule),
        typeof(HelloApplicationContractsModule),
        typeof(PrivateMessagingHttpApiModule),
        typeof(BloggingHttpApiModule),
        typeof(SettingUiHttpApiModule),
        typeof(EShopHttpApiModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule)
        )]
    public class HelloHttpApiModule : AbpModule
    {

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HelloHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);

            Configure<MvcOptions>(options =>
            {
                options.EnableEndpointRouting = false;
            });

            //Configure<CookiePolicyOptions>(options =>
            //{
            //    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            //    options.OnAppendCookie = cookieContext =>
            //        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            //    options.OnDeleteCookie = cookieContext =>
            //        CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            //});


            context.Services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

            context.Services.TryAddSingleton<ObjectPool<StringBuilder>>(serviceProvider =>
            {
                var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
                var policy = new StringBuilderPooledObjectPolicy();
                return provider.Create(policy);
            });

            context.Services.AddWebEncoders();
        }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                options.SameSite = SameSiteMode.Unspecified;
            }
        }
    }
}
