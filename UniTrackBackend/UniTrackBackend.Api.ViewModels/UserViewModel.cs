using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.ViewModels;

public class UserViewModel
{
    [Required(ErrorMessage ="Username field is required")]
    [StringLength(30, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    public required string UserName { get; set; }
    
    [Required(ErrorMessage ="First name field is required")]
    [StringLength(30, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage ="Last name field is required")]
    [StringLength(30, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    public required string LastName { get; set; }
    
    [Required(ErrorMessage ="Last name field is required")]
    [EmailAddress(ErrorMessage = "Email is not in valid format")]
    public required string Email { get; set; }
}