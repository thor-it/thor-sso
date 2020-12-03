using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Thor.SSO.CustomizationSSO
{
    [Dependency(ReplaceServices = true)]
    public class MyProjectBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SRC Thor - Single Sign-On";
    }
}
