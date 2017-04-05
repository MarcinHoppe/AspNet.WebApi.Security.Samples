using System.Web.Http;

namespace ClientCertificateAuthentication.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/user/{id}")]
        [HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            return Ok($"{id} (requested by {User?.Identity.Name})");
        }
    }
}