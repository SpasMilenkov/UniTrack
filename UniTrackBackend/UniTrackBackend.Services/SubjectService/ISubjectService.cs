using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.SubjectService;

public interface ISubjectService
{
    Task<IEnumerable<Subject>> GetAllSubjectsAsync();
    public Task<Subject> AddSubjectAsync(Subject subject);
    public Task<Subject?> GetSubjectByIdAsync(int id);
    public Task<Subject> UpdateSubjectAsync(Subject subject);
    public Task DeleteSubjectAsync(int id);
    public Task<Subject> AssignTeachersToSubject(Subject subject, List<int> teacherIds);
    public Task<Subject> AssignClassTeacherToSubject(Subject subject, int teacherId);
}