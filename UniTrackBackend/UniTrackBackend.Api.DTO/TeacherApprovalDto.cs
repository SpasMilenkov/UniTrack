using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class TeacherApprovalDto
{
    public string UserId { get; set; } = null!;
    public int ClassId { get; set; }
    public int SchoolId { get; set; }
    public List<int> SubjectIds { get; set; }
}