using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace UniTrackBackend.Services
{
    public class YouTubeSuggestionService : IYouTubeSuggestionService
    {
        private readonly YouTubeService _youtubeService;

        public YouTubeSuggestionService(string apiKey)
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<IEnumerable<YouTubeSuggestion>> GetYouTubeSuggestionsAsync(string subject)
        {
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.Q = $"{subject} tutorial"; // Customize your search query here
            searchListRequest.MaxResults = 5; // You can adjust the number of results

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var suggestions = new List<YouTubeSuggestion>();
            foreach (var searchResult in searchListResponse.Items)
            {
                suggestions.Add(new YouTubeSuggestion
                {
                    Title = searchResult.Snippet.Title,
                    Url = $"https://www.youtube.com/watch?v={searchResult.Id.VideoId}"
                });
            }

            return suggestions;
        }
    }

    public class YouTubeSuggestion
    {
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
