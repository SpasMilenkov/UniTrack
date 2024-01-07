using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class SubjectDto
{ 
    [Required]
    public required string Name { get; set; }
    [Required]
    public required IEnumerable<int> TeacherIds { get; set; }
}