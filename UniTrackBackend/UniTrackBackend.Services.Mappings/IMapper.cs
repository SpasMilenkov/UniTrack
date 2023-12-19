using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Mappings;

public interface IMapper
{
    public Mark? MapMark(MarkDto model);

    public AbsenceResultDto MapAbsenceResultViewModel(Absence absence);

    public Absence? MapAbsence(AbsenceDto model);
    // public Student? MapStudent(StudentViewModel model);

    public StudentResultDto? MapStudentViewModel(Student student);

    public MarkDto? MapMarkViewModel(Mark mark);

    public School? MapSchool(SchoolDto model);
    public TeacherResultDto? MapTeacherViewModel(Teacher teacher, string? classId = null, string? className = null);

    public Subject? MapSubject(SubjectDto model);
    public SubjectResultDto? MapSubjectViewModel(Subject subject);
    public LoginResultDto? MapLoginResult(string userId, string role, string avatarUrl);

}