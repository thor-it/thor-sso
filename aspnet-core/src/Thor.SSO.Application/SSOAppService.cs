using System;
using System.Collections.Generic;
using System.Text;
using Thor.SSO.Localization;
using Volo.Abp.Application.Services;

namespace Thor.SSO
{
    /* Inherit your application services from this class.
     */
    public abstract class SSOAppService : ApplicationService
    {
        protected SSOAppService()
        {
            LocalizationResource = typeof(SSOResource);
        }
    }
}
