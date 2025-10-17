using Application;
using Infrastructure;
using Persistence;
using Api.Middleware;
using Api.Extensions;
using Serilog;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureSwagger();
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();
builder.Services.AddHttpContextAccessor();
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
    app.AddMigrations();
}
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionLocalizationMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
