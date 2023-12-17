using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.ViewModels;

public class TeacherApprovalViewModel
{
    public string UserId { get; set; } = null!;
    public int ClassId { get; set; }
    public int SchoolId { get; set; }
    public List<int> SubjectIds { get; set; }
    public List<int> GradeIds { get; set; }
}