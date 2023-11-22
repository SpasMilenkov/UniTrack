using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.ViewModels;

public class StudentViewModel
{
    // public int StudentId { get; set; }

    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    

    [Required]
    [StringLength(10)]
    public required string Grade { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }
    

}