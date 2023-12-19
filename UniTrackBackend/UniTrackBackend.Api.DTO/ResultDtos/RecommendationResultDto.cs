namespace UniTrackBackend.Api.DTO.ResultDtos;

public class RecommendationResultDto
{
    public required string Title { get; set; }
    public required string Link { get; set; }
    public required string Thumbnail { get; set; }
    public required DateTimeOffset? PublishedDate { get; set; }
}