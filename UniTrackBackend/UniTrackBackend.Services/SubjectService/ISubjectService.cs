using UniTrackBackend.Api.DTO;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.SubjectService;

public interface ISubjectService
{
    Task<IEnumerable<Subject>> GetAllSubjectsAsync();
    Task<Subject> AddSubjectAsync(SubjectDto subjectDto);
    public Task<Subject?> GetSubjectByIdAsync(int id);
    Task<Subject> UpdateSubjectAsync(int id, SubjectDto subjectDto);
    public Task DeleteSubjectAsync(int id);
    Task<Subject> AssignClassTeacherToSubject(int subjectId, int teacherId);
}