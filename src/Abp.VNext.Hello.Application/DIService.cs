﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using System;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace Abp.VNext.Hello
{
    public class DIService : ApplicationService
    {
        IAbpApplication AbpApplication { get; }
        public DIService(ILoggerFactory loggerFactory, IAbpApplication abpApplication)
        {
            AbpApplication = abpApplication;
        }

        public ContainerBuilder GetContainer()
        {
            ObjectAccessor<ContainerBuilder> item = LazyServiceProvider.LazyGetRequiredService<ObjectAccessor<ContainerBuilder>>();
            return item.Value;
        }

        public IApplicationBuilder GetApplicationBuilder()
        {
  
            IApplicationBuilder appBuilder = LazyServiceProvider.LazyGetRequiredService<IApplicationBuilder>();
            return appBuilder;
        }

        public IEnumerable<object> GetServiceProvider()
        {
            IServiceProvider provider = LazyServiceProvider.LazyGetRequiredService<IServiceProvider>();
            if (provider is AutofacServiceProvider)
            {
                AutofacServiceProvider autofacServiceProvider = provider as AutofacServiceProvider;
                var registrations = from reg in autofacServiceProvider.LifetimeScope.ComponentRegistry.Registrations
                                    select new
                                    {
                                        Descriptions = reg.Services.Select(x => x.Description),
                                        reg.Metadata,
                                        reg.Ownership,
                                        reg.Id,
                                        Target = new
                                        {
                                            reg.Target.Id,
                                            Descriptions = reg.Target.Services.Select(x => x.Description)
                                        }
                                    };
                return registrations.OrderBy(x => x.Descriptions.Count());
            }
            return null;
        }
    }
}
