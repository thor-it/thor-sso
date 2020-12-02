using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Thor.SSO.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(SSOHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class SSOConsoleApiClientModule : AbpModule
    {
        
    }
}
