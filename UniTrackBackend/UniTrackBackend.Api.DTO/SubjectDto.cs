namespace UniTrackBackend.Api.DTO;

public class SubjectDto
{ 
    public required string Name { get; set; }
    public required IEnumerable<int> TeacherIds { get; set; }
}