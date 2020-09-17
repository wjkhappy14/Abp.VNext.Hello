using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using System;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Collections.Generic;
using System.Linq;

namespace Abp.VNext.Hello
{
    public class DIService : ApplicationService
    {
        public DIService(ILoggerFactory loggerFactory)
        {

        }

        public ContainerBuilder GetContainer()
        {
            ObjectAccessor<ContainerBuilder> item = ServiceProvider.GetRequiredService<ObjectAccessor<ContainerBuilder>>();
            return item.Value;
        }

        public IApplicationBuilder GetApplicationBuilder()
        {
  
            IApplicationBuilder appBuilder = ServiceProvider.GetRequiredService<IApplicationBuilder>();
            return appBuilder;
        }

        public IEnumerable<object> GetServiceProvider()
        {
            IServiceProvider provider = ServiceProvider.GetRequiredService<IServiceProvider>();
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
