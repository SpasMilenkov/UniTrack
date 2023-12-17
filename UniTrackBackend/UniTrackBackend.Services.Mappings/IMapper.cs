using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Mappings;

public interface IMapper
{
    public Mark? MapMark(MarkViewModel model);
    public Absence? MapAbsence(AbsenceViewModel model);
    // public Student? MapStudent(StudentViewModel model);

    public StudentResultViewModel? MapStudentViewModel(Student student);

    public MarkViewModel? MapMarkViewModel(Mark mark);
    public AbsenceViewModel MapAbsenceViewModel(Absence absence);

    public School? MapSchool(SchoolViewModel model);
    public TeacherResultViewModel? MapTeacherViewModel(Teacher teacher, string? classId = null, string? className = null);

    public Subject? MapSubject(SubjectViewModel model);
    public SubjectResultViewModel? MapSubjectViewModel(Subject subject);
    public LoginResultViewModel? MapLoginResult(string userId, string role, string avatarUrl);
}