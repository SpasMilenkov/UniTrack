using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class StudentApprovalDto
{
    [Required]
    public List<string> StudentIds { get; set; }
    [Required]
    public int GradeId { get; set; }
    [Required]
    public int SchoolId { get; set; }
}