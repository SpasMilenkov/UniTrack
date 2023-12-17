namespace UniTrackBackend.Api.ViewModels;

public class SubjectViewModel
{ 
    public required string Name { get; set; }
    public required IEnumerable<int> TeacherIds { get; set; }
}