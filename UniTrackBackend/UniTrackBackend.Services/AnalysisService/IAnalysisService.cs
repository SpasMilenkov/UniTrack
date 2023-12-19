using UniTrackBackend.Api.DTO.ResultDtos;

namespace UniTrackBackend.Services;

public interface IAnalysisService
{
    public Task<StudentAnalysisResultDto?> GenerateAnalysisModel(int studentId);
}