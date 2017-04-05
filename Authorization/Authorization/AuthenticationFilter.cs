using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Authorization
{
    public class AuthenticationFilter : IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context.Request.Headers.Contains("Login")
                && context.Request.Headers.Contains("Role"))
            {
                var login = context.Request.Headers.GetValues("Login").First();
                var role = context.Request.Headers.GetValues("Role").First();

                var identity = new GenericIdentity(login);

                context.Principal = new GenericPrincipal(identity, new[] { role });

                Console.WriteLine("User authenticated.");
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}