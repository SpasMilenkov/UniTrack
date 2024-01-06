using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(30, ErrorMessage = "Email cannot be longer than 100 characters.")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters long.")]
    public required string Password { get; set; }
}   