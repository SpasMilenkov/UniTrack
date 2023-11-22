using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data.Models.TypeSafe;

namespace UniTrackBackend.Data.Seeding;

public static class DataSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
        
        await SeedGradesAsync(unitOfWork);
        await SeedRolesAsync(roleManager);
        
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

    private static async Task SeedGradesAsync(UnitOfWork unitOfWork)
    {
        var grade = new Grade
        {
            Name = "8a"
        };
        await unitOfWork.GradeRepository.AddAsync(grade);
        await unitOfWork.SaveAsync();
    }

    private static async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
