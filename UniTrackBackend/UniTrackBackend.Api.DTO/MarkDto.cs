using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class MarkDto
{
    [Required]
    public decimal Value { get; set; }
    [Required]
    public int StudentId { get; set; }
    [Required]
    public int TeacherId { get; set; }
    [Required]
    public int SubjectId { get; set; }
    [Required]
    public required string Topic { get; set; }
    [Required]
    public DateTime GradedOn { get; set; }
}