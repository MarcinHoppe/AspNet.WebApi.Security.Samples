using System.Web.Http;
using System.Web.Http.Cors;

namespace CORS.Controllers
{
    [EnableCors("http://localhost:61520", "*", "*")]
    public class EchoController : ApiController
    {
        [Route("api/echo")]
        [HttpGet]
        public string GetEcho()
        {
            return "Hello, CORS!";
        }
    }
}