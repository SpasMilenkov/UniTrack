using Microsoft.AspNetCore.Identity;

namespace UniTrackBackend.Data.Models;

public class User : IdentityUser
{
    public required string FirstName { get; set; } = null!;

    public required string LastName { get; set; } = null!;

    public string? RefreshToken { get; set; } = null!;
    
    public DateTime? RefreshTokenValidity { get; set; }

    public string AvatarUrl { get; set; } = null!;
}