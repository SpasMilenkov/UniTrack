using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IMarkRepository : IRepository<Mark>
{
    Task<List<ClassAverage>> CalculateClassAverages(int gradeId);
    Task<List<Mark>> GetMarksWithDetailsByStudent(int studentId);
    Task<List<Mark>> GetMarksWithDetailsByTeacher(int teacherId);
}