using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CSRF.Middleware
{
    public class AntiForgeryAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var cookie = actionContext
                .Request
                .Headers
                .GetCookies(AntiForgeryConfig.CookieName)
                .FirstOrDefault();

            if (cookie == null || cookie.Cookies.Count == 0)
                return false;

            IEnumerable<string> headerValues;
            if (!actionContext
                .Request
                .Headers
                .TryGetValues("__RequestVerificationToken", out headerValues))
                return false;

            if (headerValues == null || !headerValues.Any())
                return false;

            try
            {
                AntiForgery.Validate(
                    cookie.Cookies.First().Value,
                    headerValues.First());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}