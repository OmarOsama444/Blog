using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class HeaderToClaimsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HeaderToClaimsMiddleware> _logger;

        public HeaderToClaimsMiddleware(RequestDelegate next, ILogger<HeaderToClaimsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var claims = new List<Claim>();

            // Example: Map X-User-Id → claim "user_id"
            if (context.Request.Headers.TryGetValue("X-User-Id", out var userId))
            {
                claims.Add(new Claim("user_id", userId!));
            }

            // Example: Map X-User-Role → claim "role"
            if (context.Request.Headers.TryGetValue("X-Roles", out var rolesHeader))
            {
                var roles = rolesHeader
                    .ToString()
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (var r in roles)
                    claims.Add(new Claim(ClaimTypes.Role, r));
            }


            // Example: Map X-Email → claim "email"
            if (context.Request.Headers.TryGetValue("X-Email", out var email))
            {
                claims.Add(new Claim(ClaimTypes.Email, email!));
            }

            if (claims.Any())
            {
                var identity = new ClaimsIdentity(claims, "HeaderAuth");
                context.User = new ClaimsPrincipal(identity);

                _logger.LogInformation("Mapped headers to claims for request: {Claims}",
                    string.Join(", ", claims.Select(c => $"{c.Type}={c.Value}")));
            }

            await _next(context);
        }
    }

}