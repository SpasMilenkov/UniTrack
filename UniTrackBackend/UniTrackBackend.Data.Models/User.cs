using Microsoft.AspNetCore.Identity;

namespace UniTrackBackend.Data.Models;

public class User : IdentityUser
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenValidity { get; set; }
}