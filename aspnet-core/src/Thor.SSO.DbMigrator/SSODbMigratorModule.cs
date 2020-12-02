using Thor.SSO.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Thor.SSO.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SSOEntityFrameworkCoreDbMigrationsModule),
        typeof(SSOApplicationContractsModule)
        )]
    public class SSODbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
