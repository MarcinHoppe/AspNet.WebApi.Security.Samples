This sample application uses client certificate subject name to authenticate the user and create a principal object:

```cs
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
```

## Setting up TLS in IIS (prerequisite)

This Microsoft documentation page explains how to configure IIS to host Web API over TLS:

[Working with SSL in Web API](https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/working-with-ssl-in-web-api)

The page also contains instructions on how to generate a client certificate for testing.