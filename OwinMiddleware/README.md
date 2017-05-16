This sample shows a simple logging middleware that illustrates how to hook into the OWIN processing pipeline:

```cs
public class LoggingMiddleware : Microsoft.Owin.OwinMiddleware
{
    public LoggingMiddleware(Microsoft.Owin.OwinMiddleware next) : base(next)
    {
    }

    public override async Task Invoke(IOwinContext context)
    {
        Console.WriteLine($"Processing request at {context.Environment["owin.RequestPath"]}");

        await Next.Invoke(context);

        Console.WriteLine("Done processing.");
    }
}
```
