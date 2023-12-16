using UniTrackBackend.Api.ViewModels.ResultViewModels;

namespace UniTrackBackend.Services;

public interface IAnalysisService
{
    public Task<StudentAnalysisResultViewModel?> GenerateAnalysisModel(int studentId);
}