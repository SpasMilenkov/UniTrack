using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class StudentApprovalDto
{
    public List<string> StudentIds { get; set; }
    public int GradeId { get; set; }
    public int SchoolId { get; set; }
}