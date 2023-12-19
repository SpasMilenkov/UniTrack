using UniTrackBackend.Api.DTO.ResultDtos;

namespace UniTrackBackend.Services;

public interface IRecommendationService
{
    public Task<IEnumerable<RecommendationResultDto>?> GetRecommendations(int studentId);
}