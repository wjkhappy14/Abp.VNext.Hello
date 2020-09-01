using System.IO;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Abp.VNext.Hello.EntityFrameworkCore;
using Abp.VNext.Hello.Localization;
using Abp.VNext.Hello.MultiTenancy;
using Abp.VNext.Hello.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Microsoft.AspNetCore.SignalR;
using System;
using Volo.Abp.Timing;
using Serilog;
using EasyAbp.PrivateMessaging.Web;
using EasyAbp.PrivateMessaging;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Volo.Blogging;
using Volo.Abp.AspNetCore.SignalR;
using EasyAbp.EShop.Stores.Web;
using EasyAbp.Abp.SettingUi.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Extensions;

namespace Abp.VNext.Hello.Web
{
    [DependsOn(
        typeof(HelloHttpApiModule),
        typeof(EShopStoresWebModule),
        typeof(HelloApplicationModule),
        typeof(PrivateMessagingWebModule),
        typeof(BloggingWebModule),
        typeof(HelloEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAutofacModule),
        typeof(SettingUiWebModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(AbpAspNetCoreSerilogModule)
        )]
    public class HelloWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(HelloResource),
                    typeof(HelloDomainModule).Assembly,
                    typeof(HelloDomainSharedModule).Assembly,
                    typeof(HelloApplicationModule).Assembly,
                    typeof(HelloApplicationContractsModule).Assembly,
                    typeof(HelloWebModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddObjectAccessor<IHubContext<NotificationHub>>();
            IWebHostEnvironment hostingEnvironment = context.Services.GetHostingEnvironment();
            IConfiguration configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureCors(context, configuration);
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);

            context.Services.AddConnections();
            context.Services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(5);
            });

            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            //http://www.identityserver.com.cn/
            context.Services.AddAuthentication()
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "Hello";
                });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<HelloWebModule>();

            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VNext.Hello.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VNext.Hello.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VNext.Hello.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VNext.Hello.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<HelloWebModule>(hostingEnvironment.ContentRootPath);
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<HelloResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));

            });
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy("Default", builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new HelloMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.MinifyGeneratedScript = true;
                options.ConventionalControllers.Create(typeof(PrivateMessagingApplicationModule).Assembly);
                options.ConventionalControllers.Create(typeof(HelloApplicationModule).Assembly,
                  opts =>
                  {
                      opts.RootPath = "hello/angkor";
                  });
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        /// <summary>
        /// ASP.NET Core 中间件
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            IApplicationBuilder app = context.GetApplicationBuilder();
            IWebHostEnvironment env = context.GetEnvironment();
            //https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0
            app.UseCorrelationId();

            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            {
                //开发人员异常页 
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseErrorPage();
            }
            //启用目录浏览
            app.UseDirectoryBrowser();
            //静态文件
            app.UseStaticFiles();
            //状态码页面
            app.UseStatusCodePages();
            app.UseVirtualFiles();
            app.UseRouting();

            app.UseCors("Default");
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseAbpRequestLocalization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["Now"] = $"{DateTime.Now.ToLocalTime()}";
                    return Task.CompletedTask;
                });
                await next();
            });

            app.Map("/time", time =>
            {
                //终止中间件
                time.Run(async context =>
                {
                    await context
                      .Response
                      .WriteAsync($"{DateTime.Now.ToLocalTime()}");
                });
            });
            app.Run(async (context) =>
            {
                //https://docs.microsoft.com/zh-cn/archive/msdn-magazine/2019/june/cutting-edge-revisiting-the-asp-net-core-pipeline
                await context.Response.WriteJsonAsync(context.Request.Path);
            });
        }
    }
}
