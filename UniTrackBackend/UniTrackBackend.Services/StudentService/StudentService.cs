using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Student?> AddStudentAsync(Student? student)
    {
        await _unitOfWork.StudentRepository.AddAsync(student);
        await _unitOfWork.SaveAsync();
        return student;
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        var student = await _unitOfWork.StudentRepository.GetStudentWithDetailsAsync(id);
        return student;
    }

    public async Task<Student?> GetStudentByUserIdAsync(string userId)
    {
        var student = await _unitOfWork.StudentRepository.GetStudentWithDetailsAsync(userId);
        return student;
    }

    public async Task<IEnumerable<Student?>> GetAllStudentsAsync()
    {
        return await _unitOfWork.StudentRepository.GetAllAsync();
    }

    public async Task UpdateStudentAsync(Student? student)
    {
        await _unitOfWork.StudentRepository.UpdateAsync(student);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
                
        if (student == null) return false;
            
        await _unitOfWork.StudentRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
        return true;

    }
}