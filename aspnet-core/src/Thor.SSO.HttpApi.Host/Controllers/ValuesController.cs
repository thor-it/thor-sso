using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Thor.SSO.Controllers
{
    public class ValuesController : SSOController
    {
        [Authorize, HttpGet]
        public string ResponseEndpoint()
        {
            return "Jaap ID4 is working like a charm (now with Docker & ABP)!!";
        }
    }
}
