using Flowtap_Infrastructure.Persistence.DbContext;
using Flowtap_Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Flowtap_Infrastructure.Persistence;

/// <summary>
/// Runs once at application startup to seed reference data
/// (PermissionCategories, Permissions).
/// Idempotent — safe to run on every startup.
/// </summary>
public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services, ILogger logger)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            // Apply any pending migrations automatically
            await db.Database.MigrateAsync();

            await SeedPermissionsAsync(db, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    // ─── Permissions ──────────────────────────────────────────────────────────
    private static async Task SeedPermissionsAsync(ApplicationDbContext db, ILogger logger)
    {
        // Seed categories
        var existingCategoryIds = await db.PermissionCategories
            .Select(c => c.Id)
            .ToListAsync();

        var newCategories = PermissionSeedData.GetCategories()
            .Where(c => !existingCategoryIds.Contains(c.Id))
            .ToList();

        if (newCategories.Count > 0)
        {
            db.PermissionCategories.AddRange(newCategories);
            await db.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} permission categories.", newCategories.Count);
        }

        // Seed permissions (keyed by unique Key string to avoid duplicates)
        var existingKeys = await db.Permissions
            .Select(p => p.Key)
            .ToListAsync();

        var newPermissions = PermissionSeedData.GetPermissions()
            .Where(p => !existingKeys.Contains(p.Key))
            .ToList();

        if (newPermissions.Count > 0)
        {
            db.Permissions.AddRange(newPermissions);
            await db.SaveChangesAsync();
            logger.LogInformation("Seeded {Count} permissions.", newPermissions.Count);
        }
    }
}
