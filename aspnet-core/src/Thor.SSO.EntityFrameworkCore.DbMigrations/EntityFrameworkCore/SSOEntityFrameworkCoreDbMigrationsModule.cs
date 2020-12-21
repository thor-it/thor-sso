using Microsoft.Extensions.DependencyInjection;
using Aguacongas.AspNetCore.Authentication.EntityFramework;
using Volo.Abp.Modularity;

namespace Thor.SSO.EntityFrameworkCore
{
    [DependsOn(
        typeof(SSOEntityFrameworkCoreModule)
        )]
    public class SSOEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services
                .AddAbpDbContext<SSOMigrationsDbContext>()
                // Dynamic Authentication DbContext
                .AddDbContext<SchemeDbContext>();
        }
    }
}
