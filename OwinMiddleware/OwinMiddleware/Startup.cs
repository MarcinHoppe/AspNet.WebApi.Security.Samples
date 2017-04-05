using System.Web.Http;
using Owin;

namespace OwinMiddleware
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();

            app.Use<LoggingMiddleware>();
            app.UseWebApi(configuration);
        }
    }
}