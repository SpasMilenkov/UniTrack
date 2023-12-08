namespace UniTrackBackend.Api.ViewModels.ResultViewModels;

public class RecommendationResultViewModel
{
    public required string Title { get; set; }
    public required string Link { get; set; }
    public required string Thumbnail { get; set; }
    public required DateTimeOffset? PublishedDate { get; set; }
}