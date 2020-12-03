using Microsoft.AspNetCore.Mvc;
using Thor.SSO.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Thor.SSO.Controllers
{
    /* Inherit your controllers from this class.
     */
    [Route("api/[controller]")]
    [ApiController]
    public abstract class SSOController : AbpController
    {
        protected SSOController()
        {
            LocalizationResource = typeof(SSOResource);
        }
    }
}