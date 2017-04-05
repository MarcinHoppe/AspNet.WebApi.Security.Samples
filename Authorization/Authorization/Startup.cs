using System.Web.Http;
using Authorization;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Authorization
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.MapHttpAttributeRoutes();

            configuration.Filters.Add(new AuthenticationFilter());

            app.UseWebApi(configuration);
        }
    }
}