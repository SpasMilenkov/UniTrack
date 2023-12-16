using UniTrackBackend.Api.ViewModels.ResultViewModels;

namespace UniTrackBackend.Services;

public interface IRecommendationService
{
    public Task<IEnumerable<RecommendationResultViewModel>?> GetRecommendations(int studentId);
}