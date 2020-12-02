using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace Thor.SSO.CustomizationSSO
{
    [Dependency(ReplaceServices = true)]
    public class MyProjectBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SRC Thor - Single Sign-On";
    }
}
