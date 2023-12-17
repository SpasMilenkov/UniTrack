using UniTrackBackend.Data.Commons;

namespace UniTrackBackend.Services;

public class GradeService : IGradeService
{
    private readonly IUnitOfWork _unitOfWork;
    public GradeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<KeyValuePair<string, string>?> GetClassTeacherData(int teacherId)
    {
        var grade = await _unitOfWork.GradeRepository.FirstOrDefaultAsync(g => g.ClassTeacherId == teacherId);
        if (grade is null)
            return null;

        return new KeyValuePair<string, string>(grade.Id.ToString(), grade.Name);
    }
}