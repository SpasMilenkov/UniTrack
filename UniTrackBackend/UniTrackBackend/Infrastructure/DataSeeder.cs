using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Models;
using UniTrackBackend.Models.TypeSafe;

namespace UniTrackBackend.Infrastructure;

public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        
        await SeedRolesAsync(roleManager);
        
        //Add users here
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.SuperAdmin);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Admin);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Guest);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Teacher);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Student);
        await EnsureRoleExistsAsync(roleManager, Ts.Roles.Parent);
    }

    private static async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
