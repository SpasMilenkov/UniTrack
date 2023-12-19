using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.DTO.ResultDtos;

public class StudentResultDto
{

    [Required] public string Id { get; set; } = null!;

    [Required]
    public string UniId { get; set; } = null!;

    [Required]
    public string ClassTeacherId { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string ClassTeacherFirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string ClassTeacherLastName { get; set; } = null!;

    [Url]
    public string AvatarUrl { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression("STUDENT")]
    public string Type { get; set; } = null!;

    [Required]
    public string ClassId { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string ClassName { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public int Number { get; set; }

    public List<MarkDto> Marks { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string? Email { get; set; } = null!;

    public List<AbsenceResultDto> Absences { get; set; } = null!;
}
