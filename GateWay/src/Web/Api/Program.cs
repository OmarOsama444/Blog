using Application;
using Infrastructure;
using Api.Middleware;
using Api.Extensions;
using Serilog;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureSwagger();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddLocalizationServices();
builder.Services.ConfigureReverseProxy(builder.Configuration);
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
app.UseLocalization();
app.UseMiddleware<ExceptionLocalizationMiddleware>();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapReverseProxy();
app.Run();
