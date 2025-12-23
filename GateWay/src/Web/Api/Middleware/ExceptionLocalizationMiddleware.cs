using System.Text;
using System.Text.Json;
using Application.Abstractions;
using Microsoft.Extensions.Localization;

namespace Api.Middleware;

public class ExceptionLocalizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionLocalizationMiddleware> _logger;
    private readonly IStringLocalizer _localizer;


    public ExceptionLocalizationMiddleware(RequestDelegate next, ILogger<ExceptionLocalizationMiddleware> logger, IStringLocalizerFactory localizerFactory)
    {
        _next = next;
        _logger = logger;
        _localizer = localizerFactory.Create("ErrorMessages", Presentation.AssemblyRefrence.Assembly.FullName!);
    }

    public async Task Invoke(HttpContext context)
    {
        var originalBody = context.Response.Body;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await _next(context);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var bodyText = await new StreamReader(memoryStream).ReadToEndAsync();

        if (context.Response.StatusCode == StatusCodes.Status409Conflict &&
            bodyText.Contains("\"args\""))
        {
            using var doc = JsonDocument.Parse(bodyText);
            var root = doc.RootElement;

            var code = root.GetProperty("code").GetString()!;
            var status = root.GetProperty("status").GetInt32();
            var argsJsonList = root.GetProperty("args").GetString();
            string[] args = JsonSerializer.Deserialize<string[]>(argsJsonList!)!;
            var newResponse = new
            {
                code,
                status,
                message = _localizer[code, args].Value.ToString()
            };

            var json = JsonSerializer.Serialize(newResponse);

            context.Response.ContentLength = json.Length;
            context.Response.ContentType = "application/json";

            await originalBody.WriteAsync(Encoding.UTF8.GetBytes(json));
            return;
        }

        memoryStream.Seek(0, SeekOrigin.Begin);
        await memoryStream.CopyToAsync(originalBody);
    }
}