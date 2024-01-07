using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, ErrorMessage = "Username cannot be longer than 30 characters.")]
    public required string UserName { get; set; }
    
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(30, ErrorMessage = "First name cannot be longer than 30 characters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(30, ErrorMessage = "Last name cannot be longer than 30 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(30, ErrorMessage = "Email cannot be longer than 30 characters.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(30, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 30 characters long.")]
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
    [Required(ErrorMessage = "School ID is required.")]
    public required int SchoolId { get; set; }
    
}