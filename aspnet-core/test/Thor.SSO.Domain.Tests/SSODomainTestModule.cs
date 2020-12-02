using Thor.SSO.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Thor.SSO
{
    [DependsOn(
        typeof(SSOEntityFrameworkCoreTestModule)
        )]
    public class SSODomainTestModule : AbpModule
    {

    }
}