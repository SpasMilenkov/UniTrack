using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public interface IStudentService
{
    Task<Student?> AddStudentAsync(Student? student);
    Task<Student?> GetStudentByIdAsync(int id);
    Task<IEnumerable<Student?>> GetAllStudentsAsync();
    Task UpdateStudentAsync(Student? student);
    Task<bool> DeleteStudentAsync(int id);
    Task<Student?> GetStudentByUserIdAsync(string userId);
    Task<IEnumerable<Student>> GetStudentsByTeacherId(int teacherId);
}