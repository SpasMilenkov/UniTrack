namespace UniTrackBackend.Api.ViewModels.ResultViewModels;

public class SubjectResultViewModel
{
    public required string Name { get; set; }
    public required IEnumerable<string> TeacherIds { get; set; }
}