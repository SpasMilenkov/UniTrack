using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher?>?> GetAllTeachersAsync();
        Task<Teacher?> GetTeacherByIdAsync(int id);
        Task<Teacher?> AddTeacherAsync(Teacher? teacher);
        Task<Teacher?> UpdateTeacherAsync(Teacher? teacher);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
