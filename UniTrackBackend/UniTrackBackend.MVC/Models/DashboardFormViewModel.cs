using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.MVC.Models;

public class DashboardFormViewModel
{
    [Required(ErrorMessage = "School name is required")]
    [StringLength(30, ErrorMessage = "School name cannot be longer than 100 characters.")]
    public string SchoolName { get; set; } = string.Empty;
}