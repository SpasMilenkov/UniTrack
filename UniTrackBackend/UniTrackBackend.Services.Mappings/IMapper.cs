using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Mappings;

public interface IMapper
{
    public Mark? MapMark(MarkViewModel model);
    public Absence? MapAbsence(AbsenceViewModel model);
    public Student? MapStudent(StudentViewModel model);

    public StudentResultViewModel? MapStudentViewModel(Student student);

    public MarkViewModel? MapMarkViewModel(Mark mark);
    public AbsenceViewModel MapAbsenceViewModel(Absence absence);

    public School? MapSchool(SchoolViewModel model);
}