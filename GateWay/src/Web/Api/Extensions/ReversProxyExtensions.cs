using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Yarp.ReverseProxy.Transforms;

namespace Api.Extensions
{
    public static class ReversProxyExtensions
    {
        public static IServiceCollection ConfigureReverseProxy(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"))
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
            return services;
        }
    }
}