using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.ViewModels;

public class StudentViewModel
{

    [Required]
    public string Id { get; set; }

    [Required]
    public string UniId { get; set; }

    [Required]
    public string ClassTeacherId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ClassTeacherFirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string ClassTeacherLastName { get; set; }

    [Url]
    public string AvatarUrl { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [RegularExpression("STUDENT")]
    public string Type { get; set; }

    [Required]
    public string ClassId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ClassName { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Number { get; set; }

    public List<MarkViewModel> Marks { get; set; }

    // public List<AbsenceViewModel> Absences { get; set; }
}
