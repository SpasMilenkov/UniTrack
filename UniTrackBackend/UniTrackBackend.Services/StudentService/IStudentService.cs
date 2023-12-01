using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.StudentService
{
    public interface IStudentService
    {
        Task<Student> AddStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
    }
}
