This sample demonstrates how to use declarative (attribute-based) role-based authorization on a controller level:

```cs
[Authorize(Roles = "admin")]
public class UserController : ApiController
{
    [Route("api/user/{id}")]
    [HttpGet]
    [OverrideAuthorization]
    [Authorize(Roles = "user")]
    public IHttpActionResult GetUser(string id)
    {
        Console.WriteLine($"Getting user for id={id}");
        Console.WriteLine($"Current user name: {User?.Identity.Name}");
        return Ok(id);
    }
}
```

Users are authenticated using a dummy authentication filter that uses custom HTTP headers (`Login` and `Role`):

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
```
