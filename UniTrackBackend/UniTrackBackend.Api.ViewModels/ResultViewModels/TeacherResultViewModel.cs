namespace UniTrackBackend.Api.ViewModels.ResultViewModels;

public class TeacherResultViewModel
{
    public required string Id { get; set; }
    public required string UniId { get; set; }
    public required string AvatarUrl { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; } 
    public required string Type { get; set; } = "TEACHER";
    public required string ClassId { get; set; }
    public required string ClassName { get; set; }
    public required List<string> Subjects { get; set; }
    // public List<ClassInfo> Classes { get; set; }
} 