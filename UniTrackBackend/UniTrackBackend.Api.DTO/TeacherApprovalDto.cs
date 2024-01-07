using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class TeacherApprovalDto
{
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public int ClassId { get; set; }
    [Required]
    public int SchoolId { get; set; }
    [Required]
    public List<int> SubjectIds { get; set; }
}