namespace UniTrackBackend.Api.ViewModels;

public class MarkViewModel
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }
    public DateTime GradedOn { get; set; }
}