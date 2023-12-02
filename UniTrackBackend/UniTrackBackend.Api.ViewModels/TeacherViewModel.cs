using System.ComponentModel.DataAnnotations;
using UniTrackBackend.Data.Models;

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
    [Required]
    public int Id { get; set; }
    public User Type { get; set; }
    public List<Subject> Subjects { get; set; }

}