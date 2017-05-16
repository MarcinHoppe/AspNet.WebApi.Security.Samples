This sample illustrates how to generate an anti-forgery token in a view template (Razor):

```
@Html.AntiForgeryToken()
```

and how to include it in an AJAX request:

```js
$.ajax({
    url: "http://localhost:61895/api/echo",
    type: "POST",
    contentType: "application/json",
    data: JSON.stringify($("#newMessage").val()),
    headers: {
        "__RequestVerificationToken":
            $("input[name='__RequestVerificationToken']").val()
    }
}).done(function (data) {
    console.log(data);
});
```

and then how to validate it in the controller:

```cs
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

// ...

public class EchoController : ApiController
{
    private static string message = "Hello, CSRF";

    //...

    [Route("api/echo")]
    [HttpPost]
    [AntiForgery]
    public string SetHello([FromBody] string newMessage)
    {
        message = newMessage;
        return message;
    }
}
```
