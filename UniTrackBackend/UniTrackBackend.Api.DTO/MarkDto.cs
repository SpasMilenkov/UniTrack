namespace UniTrackBackend.Api.DTO;

public class MarkDto
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public DateTime GradedOn { get; set; }
}