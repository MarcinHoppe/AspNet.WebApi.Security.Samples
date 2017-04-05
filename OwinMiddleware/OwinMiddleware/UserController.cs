using System;
using System.Web.Http;

namespace OwinMiddleware
{
    public class UserController : ApiController
    {
        [Route("api/user/{id}")]
        [HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            Console.WriteLine($"Getting user for id={id}");
            return Ok(id);
        }
    }
}
    