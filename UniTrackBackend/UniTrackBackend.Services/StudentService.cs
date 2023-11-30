using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Data;

namespace UniTrackBackend.Services
{
    public class StudentService : IStudentService
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.SaveAsync();
            return student;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _unitOfWork.StudentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _unitOfWork.StudentRepository.GetAllAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _unitOfWork.StudentRepository.UpdateAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student != null)
            {
                await _unitOfWork.StudentRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
