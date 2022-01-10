using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abp.VNext.Hello.Data;
using Volo.Abp.DependencyInjection;

namespace Abp.VNext.Hello.EntityFrameworkCore
{
    public class EntityFrameworkCoreHelloDbSchemaMigrator
        : IHelloDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreHelloDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the HelloDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<HelloDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
