namespace UniTrackBackend.Services
{
    public interface IYouTubeSuggestionService
    {
        Task<IEnumerable<YouTubeSuggestion>> GetYouTubeSuggestionsAsync(string subject);
    }

}
