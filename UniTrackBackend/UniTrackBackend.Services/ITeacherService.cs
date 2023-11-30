using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services
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
