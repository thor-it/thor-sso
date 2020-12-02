using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Thor.SSO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [Authorize, HttpGet]
        public string ResponseEndpoint()
        {
            return "Jaap ID4 is working like a charm (now with Docker & ABP)!!";
        }
    }
}
