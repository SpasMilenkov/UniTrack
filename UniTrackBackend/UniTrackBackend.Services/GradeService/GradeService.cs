using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Services;

public class GradeService : IGradeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GradeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<KeyValuePair<string, string>?> GetClassTeacherData(int teacherId)
    {
        var grade = await _unitOfWork.GradeRepository.FirstOrDefaultAsync(g => g.ClassTeacherId == teacherId);
        if (grade is null)
            return null;

        return new KeyValuePair<string, string>(grade.Id.ToString(), grade.Name);
    }

    public async Task<IEnumerable<GradeResultDto>> GetAllGradesBySchoolId(int schoolId)
    {
        var grades = await _unitOfWork.GradeRepository.GetGradesWithDetails(g => g.SchoolId == schoolId);
        var gradeDtos = new List<GradeResultDto>();

        foreach (var g in grades)
        {
            var studentDtos = await GetStudentByGrade(g.Id);
            var gradeResultDto = new GradeResultDto(
                g.Id.ToString(),
                g.Name,
                studentDtos,
                _mapper.MapTeacherDto(g.ClassTeacher, g.Id.ToString(), g.Name)
            );

            gradeDtos.Add(gradeResultDto);
        }

        return gradeDtos;
    }

    private async Task<IEnumerable<StudentResultDto>> GetStudentByGrade(int gradeId)
    {

        var students = await _unitOfWork.StudentRepository
            .GetStudentsWithDetailsAsync(s => s.GradeId == gradeId);
        return students.Select(s => _mapper.MapStudentDto(s));

    }
    
}