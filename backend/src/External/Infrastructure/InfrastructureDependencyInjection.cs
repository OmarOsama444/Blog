using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Infrastructure.Authentication;
using Infrastructure.Delegates;
using Infrastructure.Clients;
using Infrastructure.Options;
using Infrastructure.Services;
using Application.Interfaces;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeyCloakOptions>(configuration.GetSection("KeyCloak"));
        services.AddTransient<AdminKeyCloakAuthDelegatingHandler>();
        services.AddHttpClient<AdminKeyCloakClient>((sp, client) =>
        {
            KeyCloakOptions options = sp.GetRequiredService<IOptions<KeyCloakOptions>>().Value;
            client.BaseAddress = new Uri(options.AdminUrl);
        })
        .AddHttpMessageHandler<AdminKeyCloakAuthDelegatingHandler>();
        services.AddHttpClient<TokenKeyCloackCLient>((sp, client) =>
        {
            KeyCloakOptions options = sp.GetRequiredService<IOptions<KeyCloakOptions>>().Value;
            client.BaseAddress = new Uri(options.TokenUrl);
        });
        services.AddHttpClient<SemanticModelClient>((sp, client) =>
        {
            string baseUrl = configuration.GetConnectionString("SemanticModelUrl")!;
            client.BaseAddress = new Uri(baseUrl);
        });
        services.AddScoped<IIdentityProviderService, IdentityProviderService>();
        services.AddScoped<IEmbeddingService, EmbeddingService>();
        services.AddScoped<IClaimsTransformation, KeyCloackClaimsTransformation>();
        services.AddAuthenticationInternal();
        return services;
    }
}
