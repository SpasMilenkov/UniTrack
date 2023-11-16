using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniTrackBackend.Models;

namespace UniTrackBackend.Database;

public class UniTrackDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _configuration;

    public UniTrackDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_configuration.GetConnectionString("Database"));
    }
}
