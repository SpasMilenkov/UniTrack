using System.ComponentModel.DataAnnotations;
using UniTrackBackend.Services.Commons.Attributes;

namespace UniTrackBackend.Api.ViewModels;

public class ParentViewModel
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public required string Email { get; set; }
    
    [Required]
    [EmailList]
    public required List<string> ChildrenEmails { get; set; }
}