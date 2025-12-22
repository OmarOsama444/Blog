using Application;
using Infrastructure;
using Api.Middleware;
using Api.Extensions;
using Serilog;
using Presentation;
using Yarp.ReverseProxy.Transforms;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureSwagger();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(context =>
    {
        context.AddRequestTransform(async transformContext =>
        {
            var httpContext = transformContext.HttpContext;
            var user = httpContext.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst("sub")?.Value;
                var email = user.FindFirst("email")?.Value;
                var role = user.FindAll("role");

                if (!string.IsNullOrEmpty(userId))
                    transformContext.ProxyRequest.Headers.Add("X-User-Id", userId);

                if (!string.IsNullOrEmpty(email))
                    transformContext.ProxyRequest.Headers.Add("X-User-Email", email);

                if (!role.IsNullOrEmpty())
                    transformContext.ProxyRequest.Headers.Add("X-User-Role", JsonSerializer.Serialize(role.ToList()));
            }
        });
    });
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();
var app = builder.Build();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionLocalizationMiddleware>();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapReverseProxy();
app.Run();
