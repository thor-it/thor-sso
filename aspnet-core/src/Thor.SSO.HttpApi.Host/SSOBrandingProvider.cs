using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Thor.SSO
{
    [Dependency(ReplaceServices = true)]
    public class SSOBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SRC Thor - Single Sign-On";
    }
}
