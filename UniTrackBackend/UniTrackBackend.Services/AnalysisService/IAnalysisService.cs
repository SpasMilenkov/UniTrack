using UniTrackBackend.Api.ViewModels.ResultViewModels;

namespace UniTrackBackend.Services.AnalysisService;

public interface IAnalysisService
{
    public Task<StudentAnalysisResultViewModel?> GenerateAnalysisModel(int studentId);
}