using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Thor.SSO.Data
{
    /* This is used if database provider does't define
     * ISSODbSchemaMigrator implementation.
     */
    public class NullSSODbSchemaMigrator : ISSODbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}