using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services
{
    public interface IMarkService
    {
        Task AddMarkAsync(Mark? mark);
        Task<MarkResultDto> GetMarkByIdAsync(int id);
        Task<IEnumerable<MarkResultDto>> GetAllMarksAsync();

        Task<IEnumerable<MarkResultDto>> GetMarksByStudentAsync(int studentId);
        Task<IEnumerable<MarkResultDto>> GetMarksByTeacherAsync(int teacherId);
        Task<IEnumerable<MarkResultDto>> GetMarksBySubjectAsync(int subjectId);
        Task<IEnumerable<MarkResultDto>> GetMarksByDateAsync(DateTime date);

        Task<MarkResultDto> UpdateMarkAsync(Mark mark, int markId);
        Task DeleteMarkAsync(int id);
        
    }
}