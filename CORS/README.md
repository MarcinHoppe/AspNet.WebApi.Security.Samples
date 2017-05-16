This sample demonstrates how to enable CORS in a WebAPI endpoint:

```cs
public static void Register(HttpConfiguration config)
{
    // Web API configuration and services
    // ...
    config.EnableCors();
}
```

And then use it on a controller:

```cs
[EnableCors("http://localhost:61520", "*", "*")]
public class EchoController : ApiController
{
    [Route("api/echo")]
    [HttpGet]
    public string GetEcho()
    {
        return "Hello, CORS!";
    }
}
```
