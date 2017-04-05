using System.Web.Http;
using Owin;

namespace Filters
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();
            configuration.Filters.Add(new AuthenticationFilter());
            configuration.Filters.Add(new AuthorizationFilter());

            app.UseWebApi(configuration);
        }
    }
}