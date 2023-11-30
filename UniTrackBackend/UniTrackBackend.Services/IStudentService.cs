using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services
{
    public interface IStudentService
    {
        Task<Student> AddStudentAsync(Student student);
        Task<Student> GetStudentByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }
}
