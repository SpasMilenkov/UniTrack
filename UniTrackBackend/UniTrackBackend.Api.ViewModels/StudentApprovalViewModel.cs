using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.ViewModels;

public class StudentApprovalViewModel
{
    public List<string> StudentIds { get; set; }
    public int GradeId { get; set; }
    public int SchoolId { get; set; }
}