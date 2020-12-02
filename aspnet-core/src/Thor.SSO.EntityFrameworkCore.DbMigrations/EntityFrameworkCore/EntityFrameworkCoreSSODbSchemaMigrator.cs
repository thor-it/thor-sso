using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Thor.SSO.Data;
using Volo.Abp.DependencyInjection;

namespace Thor.SSO.EntityFrameworkCore
{
    public class EntityFrameworkCoreSSODbSchemaMigrator
        : ISSODbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreSSODbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the SSOMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<SSOMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}