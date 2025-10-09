using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Api.Extensions;

public static class MigrationsExtension
{
    public static void AddMigrations(this IApplicationBuilder application)
    {
        using var scope = application.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
        dbContext.Database.Migrate();
    }
}
