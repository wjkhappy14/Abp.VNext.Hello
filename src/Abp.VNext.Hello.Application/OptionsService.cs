using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace Abp.VNext.Hello
{
    public class OptionsService : ApplicationService
    {

        public OptionsService()
        {

        }

        public IServiceCollection GetServices()
        {
            IServiceCollection services = LazyServiceProvider.LazyGetRequiredService<IServiceCollection>();
            return services;
        }

        public IOptions<RouteOptions> GetRouteOptions()
        {
            return LazyServiceProvider.LazyGetRequiredService<IOptions<RouteOptions>>();
        }
        public KestrelServerOptions GetKestrelServerOptions()
        {
            IOptions<KestrelServerOptions> options = LazyServiceProvider.LazyGetRequiredService<IOptions<KestrelServerOptions>>();
            options.Value.ApplicationServices = null;
            options.Value.ConfigurationLoader = null;
            return options.Value;
        }

        public IdentityServerOptions GetIdentityServerOptions()
        {
            IOptions<IdentityServerOptions> options = LazyServiceProvider.LazyGetRequiredService<IOptions<IdentityServerOptions>>();
            return options.Value;
        }
        public CorsOptions GetCorsOptions()
        {
            IOptions<CorsOptions> options = LazyServiceProvider.LazyGetRequiredService<IOptions<CorsOptions>>();
            return options.Value;
        }

        public object GetMvcOptions()
        {
            var options = LazyServiceProvider.LazyGetRequiredService<IOptions<MvcOptions>>();
            return new
            {
                options.Value.Filters,
                options.Value.ValueProviderFactories,
                options.Value.Conventions
            };
        }
    }
}
