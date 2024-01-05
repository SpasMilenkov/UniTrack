using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO;

public class AbsenceDto
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int TeacherId { get; set; }
    [Required]
    public int SubjectId { get; set; }

    [Required]
    [Range(0, 1, ErrorMessage = "Value must be between 0 and 1")]
    public decimal Value { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Time { get; set; }

    [Required]
    public bool Excused { get; set; }
}