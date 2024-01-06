using UniTrackBackend.Api.DTO;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services.SubjectService;

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

    public async Task<Teacher> UpdateTeacherAsync(TeacherDto teacher, int id)
    {
        var entity = await _unitOfWork.TeacherRepository.GetByIdAsync(id);
        
        if (entity is null) throw new ArgumentException(nameof(entity));
        
        await _unitOfWork.TeacherRepository.LoadReferenceAsync(entity, e => e.User);
        var grade = await _unitOfWork.GradeRepository.GetByIdAsync(teacher.ClassId);
        
        if(grade is null) throw new AggregateException(nameof(grade));
        
        grade.ClassTeacherId = entity.Id;
        
        entity.User.FirstName = teacher.FirstName;
        entity.User.LastName = teacher.LastName;
        await _unitOfWork.TeacherRepository.UpdateAsync(entity);
        await _unitOfWork.SaveAsync();
        return entity;
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

    public async Task<GradeDto?> GetGradeByClassTeacherId(int id)
    {
        try
        {
            var grade = await _unitOfWork.GradeRepository.FirstOrDefaultAsync(g => g.ClassTeacherId == id);
            if (grade == null) throw new ArgumentException(nameof(grade));
            return new GradeDto(grade.Id, grade.Name);
        }
        catch (ArgumentException e)
        {
            return new GradeDto(-1, "Not a class teacher");
        }
    }
}