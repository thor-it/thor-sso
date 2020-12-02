using Volo.Abp.Modularity;

namespace Thor.SSO
{
    [DependsOn(
        typeof(SSOApplicationModule),
        typeof(SSODomainTestModule)
        )]
    public class SSOApplicationTestModule : AbpModule
    {

    }
}