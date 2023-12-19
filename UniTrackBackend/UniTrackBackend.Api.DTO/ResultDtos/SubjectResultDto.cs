namespace UniTrackBackend.Api.DTO.ResultDtos;

public class SubjectResultDto
{
    public required string Name { get; set; }
    public required IEnumerable<string> TeacherIds { get; set; }
}