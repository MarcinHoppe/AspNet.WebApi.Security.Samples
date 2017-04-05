using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSRF.Middleware;

namespace CSRF.Controllers
{
    public class EchoController : ApiController
    {
        private static string message = "Hello, CSRF";

        [Route("api/echo")]
        [HttpGet]
        public string GetHello()
        {
            return message;
        }

        [Route("api/echo")]
        [HttpPost]
        [AntiForgery]
        public string SetHello([FromBody] string newMessage)
        {
            message = newMessage;
            return message;
        }
    }
}
