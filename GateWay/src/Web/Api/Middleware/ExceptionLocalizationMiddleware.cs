using System.Text.Json;
using Application.Abstractions;

namespace Api.Middleware;

public class ExceptionLocalizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionLocalizationMiddleware> _logger;

    public ExceptionLocalizationMiddleware(RequestDelegate next, ILogger<ExceptionLocalizationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // var originalBodyStream = context.Response.Body;

        // await using var responseBody = new MemoryStream();
        // context.Response.Body = responseBody;
        await _next(context);
        // int statusCode = context.Response.StatusCode;
        // if (context.Response.ContentType?.Contains("application/json") == true)
        // {
        //     responseBody.Seek(0, SeekOrigin.Begin);
        //     var bodyText = await new StreamReader(responseBody).ReadToEndAsync();

        //     // Deserialize original response
        //     var json = JsonDocument.Parse(bodyText);
        //     var root = json.RootElement;

        //     // Example: alter the response
        //     var modified = new
        //     {
        //         code = root.GetProperty("code").GetInt32(),
        //         status = root.GetProperty("status").GetInt32(),
        //         message = root.GetProperty("args").GetString(),
        //         timestamp = DateTime.UtcNow,
        //         traceId = context.TraceIdentifier
        //     };

        //     context.Response.Body = originalBodyStream;
        //     context.Response.ContentLength = null;

        //     await context.Response.WriteAsJsonAsync(modified);
        //     return;
        // }

        // // Non-JSON → just pass throuxgh
        // responseBody.Seek(0, SeekOrigin.Begin);
        // await responseBody.CopyToAsync(originalBodyStream);
    }
}