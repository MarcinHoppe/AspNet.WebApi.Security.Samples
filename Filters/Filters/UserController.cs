using System;
using System.Web.Http;

namespace Filters
{
    public class UserController : ApiController
    {
        [Route("api/user/{id}")]
        [HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            Console.WriteLine($"Getting user for id={id}");
            Console.WriteLine($"Current user name: {User?.Identity.Name}");
            return Ok(id);
        }
    }
}