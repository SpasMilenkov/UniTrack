using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;

    public TeacherService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
    {
        return await _unitOfWork.TeacherRepository.GetAllTeachersWithDetailsAsync();
    }

    public async Task<Teacher> GetTeacherByIdAsync(int id)
    {
        var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
        await _unitOfWork.TeacherRepository.LoadCollectionAsync(teacher, t => t.Subjects);
        await _unitOfWork.TeacherRepository.LoadReferenceAsync(teacher, t => t.User);
        return teacher;
    }

    public async Task<Teacher?> GetTeacherByUserIdAsync(string userId, bool additionalData)
    {
        var teachers = await _unitOfWork.TeacherRepository.GetAsync(
            filter: t => t.UserId == userId);
        var teacher = teachers.FirstOrDefault();
        
        if (teacher is null || !additionalData) return teacher ?? null;

        await _unitOfWork.TeacherRepository.LoadCollectionAsync(teacher, t => t.Subjects);
        await _unitOfWork.TeacherRepository.LoadReferenceAsync(teacher, t => t.User);

        return teacher ?? null;
    }
    public async Task<Teacher> AddTeacherAsync(Teacher teacher)
    {
        await _unitOfWork.TeacherRepository.AddAsync(teacher);
        await _unitOfWork.SaveAsync();
        return teacher;
    }

    public async Task<Teacher> UpdateTeacherAsync(Teacher teacher)
    {
        await _unitOfWork.TeacherRepository.UpdateAsync(teacher);
        await _unitOfWork.SaveAsync();
        return teacher;
    }

    public async Task DeleteTeacherAsync(int id)
    {
        var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
        if (teacher != null)
        {
            await _unitOfWork.TeacherRepository.DeleteAsync(teacher.Id);
            await _unitOfWork.SaveAsync();
        }
    }
}