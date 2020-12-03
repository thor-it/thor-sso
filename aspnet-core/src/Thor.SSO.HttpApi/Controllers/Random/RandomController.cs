using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Thor.SSO.Controllers
{
    public class RandomController : SSOController
    {
        [HttpGet]
        public int GetRandomValue()
        {
            return new Random().Next();
        }

        [HttpGet("secure")]
        [Authorize]
        public int GetRandomValueProtected()
        {
            return new Random().Next();
        }
    }
}
