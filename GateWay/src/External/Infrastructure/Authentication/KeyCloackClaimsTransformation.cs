using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Authentication
{
    public class KeyCloackClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity!;

            // Realm roles
            var realmAccess = principal.FindFirst("realm_access")?.Value;
            if (realmAccess != null)
            {
                using var doc = JsonDocument.Parse(realmAccess);
                if (doc.RootElement.TryGetProperty("roles", out var roles))
                {
                    foreach (var role in roles.EnumerateArray())
                    {
                        identity.AddClaim(
                            new Claim(ClaimTypes.Role, role.GetString()!)
                        );
                    }
                }
            }

            // Client roles
            var resourceAccess = principal.FindFirst("resource_access")?.Value;
            if (resourceAccess != null)
            {
                using var doc = JsonDocument.Parse(resourceAccess);

                foreach (var client in doc.RootElement.EnumerateObject())
                {
                    var clientName = client.Name;

                    if (!client.Value.TryGetProperty("roles", out var roles))
                        continue;

                    foreach (var role in roles.EnumerateArray())
                    {
                        identity.AddClaim(
                            new Claim(
                                ClaimTypes.Role,
                                $"{clientName}:{role.GetString()}"
                            )
                        );
                    }
                }
            }

            return Task.FromResult(principal);
        }
    }
}