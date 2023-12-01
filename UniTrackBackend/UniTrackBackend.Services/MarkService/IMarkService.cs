using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.MarkService
{
    public interface IMarkService
    {
        Task<Mark?> AddMarkAsync(Mark? mark);
        Task<Mark?> GetMarkByIdAsync(int id);
        Task<IEnumerable<Mark?>?> GetAllMarksAsync();

        Task<IEnumerable<Mark>?> GetMarksByStudentAsync(int studentId);
        Task<IEnumerable<Mark?>?> GetMarksByTeacherAsync(int teacherId);
        Task<IEnumerable<Mark>?> GetMarksBySubjectAsync(int subjectId);
        Task<IEnumerable<Mark?>?> GetMarksByDateAsync(DateTime date);

        Task<Mark?> UpdateMarkAsync(Mark? mark);
        Task<bool> DeleteMarkAsync(int id);

        // Additional methods for more specific operations
    }
}