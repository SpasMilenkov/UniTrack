using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Data.Database;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Infrastructure;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UniTrackDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));
        
        services.AddIdentity<User, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<UniTrackDbContext>();
        return services;
    }
}