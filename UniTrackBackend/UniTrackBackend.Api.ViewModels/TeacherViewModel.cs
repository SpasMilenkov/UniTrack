using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.ViewModels;

public class TeacherViewModel
{
    [Required]
    [StringLength(30)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }

    [Required]
    [Phone]
    [StringLength(20)]
    public required string PhoneNumber { get; set; }
}