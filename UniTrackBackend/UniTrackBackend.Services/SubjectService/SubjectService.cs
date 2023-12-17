using Microsoft.Extensions.Logging;
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
    public async Task<Subject> AddSubjectAsync(Subject subject)
    {
        try
        {
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
    

    public async Task<Subject> UpdateSubjectAsync(Subject subject)
    {
        try
        {
            await _unitOfWork.SubjectRepository.UpdateAsync(subject);
            await _unitOfWork.SaveAsync();
            return subject;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateSubjectAsync");
            throw;
        }
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

    public async Task<Subject> AssignTeachersToSubject(Subject subject, List<int> teacherIds)
    {
        foreach (var teacherId in teacherIds)
        {
            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(teacherId);
            if (teacher is not null)
                subject.Teachers.Add(teacher);
        }

        await _unitOfWork.SubjectRepository.UpdateAsync(subject);
        await _unitOfWork.SaveAsync();
        return subject;
    }

    public async Task<Subject> AssignClassTeacherToSubject(Subject subject, int teacherId)
    {
        throw new NotImplementedException();
    }
}
