using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.SubjectService;

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SubjectService> _logger;

    public SubjectService(IUnitOfWork unitOfWork, ILogger<SubjectService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
    {
        try
        {
            return await _unitOfWork.SubjectRepository.GetAllAsync();
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllSubjectsAsync");
            throw;
        }
    }
    public async Task<Subject?> GetSubjectByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.SubjectRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSubjectByIdAsync");
            throw;
        }
    }
    public async Task<Subject> AddSubjectAsync(SubjectDto subjectDto)
    {
        try
        {
            var filter = subjectDto.TeacherIds.ToHashSet();
            var teachers = await _unitOfWork.TeacherRepository.GetAsync(t => filter.Contains(t.Id));
            var subject = new Subject
            {
                Name = subjectDto.Name,
                Teachers = teachers.ToList()
            };
            await _unitOfWork.SubjectRepository.AddAsync(subject);
            await _unitOfWork.SaveAsync();
            return subject;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AddSubjectAsync");
            throw;
        }
    }
    

    public async Task<Subject> UpdateSubjectAsync(int id, SubjectDto subjectDto)
    {
        var entity = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        
        foreach (var teacherId in subjectDto.TeacherIds)
        {
            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(teacherId);
            if (teacher is not null)
                entity.Teachers.Add(teacher);
        }

        await _unitOfWork.SubjectRepository.UpdateAsync(entity);
        await _unitOfWork.SaveAsync();
        return entity;
    }

    public async Task DeleteSubjectAsync(int id)
    {
        try
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            if (subject != null)
            {
                await _unitOfWork.SubjectRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteSubjectAsync");
            throw;
        }
    }
    public async Task<Subject> AssignClassTeacherToSubject(int subjectId, int teacherId)
    {
        var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
        if (subject is null) throw new ArgumentException(nameof(subject));

        var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(teacherId);
        if (teacher is null) throw new ArgumentException(nameof(teacher));
        
        await _unitOfWork.SubjectRepository.LoadCollectionAsync(subject, s => s.Teachers);
        
        subject.Teachers.Add(teacher);
        await _unitOfWork.SaveAsync();
        return subject;
    }
}
