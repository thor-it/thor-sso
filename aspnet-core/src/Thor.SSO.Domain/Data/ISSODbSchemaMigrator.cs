using System.Threading.Tasks;

namespace Thor.SSO.Data
{
    public interface ISSODbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
