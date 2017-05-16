This sample uses custom HTTP headers (`Login` and `Role`) to authenticate a user and create a principal object:

```cs
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

            context.Principal = new GenericPrincipal(identity, new[] {role});

            Console.WriteLine("User authenticated.");
        }

        return Task.FromResult(0);
    }

    public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}
```

The next step is to authorize only users in a certain role:

```cs
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
            var unauthorizedResponse = actionContext.Request.CreateErrorResponse(
                HttpStatusCode.Unauthorized,
                "Add Login and Role headers.");

            return Task.FromResult(unauthorizedResponse);
        }

        if (actionContext.RequestContext.Principal.IsInRole("reader"))
        {
            return continuation();
        }

        var forbiddenResponse = actionContext.Request.CreateErrorResponse(
            HttpStatusCode.Forbidden,
            "Only users in the `reader` role are authorized.");

        return Task.FromResult(forbiddenResponse);
    }
}
```
