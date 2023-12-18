using System.ComponentModel.DataAnnotations;

namespace UniTrackBackend.Api.ViewModels.ResultViewModels;

public class AbsenceResultViewModel
{
    [Required]
    [MaxLength(100)]
    public required string Subject { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public decimal AbsenceCount { get; set; }
        
    public bool Excused { get; set; }

    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int StudentId {  get; set; } 

    [Required]
    public int TeacherId { get; set; }

    [Required]
    [MaxLength(50)]
    public required string TeacherFirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public required string TeacherLastName { get; set; }
}