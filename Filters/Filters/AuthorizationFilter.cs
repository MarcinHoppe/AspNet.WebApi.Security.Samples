using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.RequestContext.Principal == null)
            {
                var response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.Unauthorized,
                    "Add Login and Role headers.");

                return Task.FromResult(response);
            }

            if (actionContext.RequestContext.Principal.IsInRole("reader"))
            {
                return continuation();
            }
            {
                var response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.Forbidden,
                    "Only users in the `reader` role are authorized.");

                return Task.FromResult(response);
            }
        }
    }
}