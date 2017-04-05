using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace ClientCertificateAuthentication.Filters
{
    public class ClientCertificateAuthenticationFilter : IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var clientCertificate = context.Request.GetClientCertificate();
            if (clientCertificate != null)
            {
                var identity = new GenericIdentity(
                    clientCertificate.SubjectName.Name);
                var roles = new[] {"Admin", "Reader"};

                context.Principal = new GenericPrincipal(identity, roles);
            }

            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}