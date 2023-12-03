using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task<Teacher> AddTeacherAsync(Teacher teacher);
        Task<Teacher> UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
    }
}
