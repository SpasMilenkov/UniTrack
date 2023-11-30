namespace UniTrackBackend.Api.ViewModels;

public class MarkViewModel
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public string Student { get; set; } = null!;
    public string Teacher { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public DateTime GradedOn { get; set; }
}