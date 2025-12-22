using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Authentication.Config;

namespace Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Authenticated", policy =>
                policy.RequireAuthenticatedUser());
        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();

        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        services.AddHttpContextAccessor();
        return services;
    }
}
