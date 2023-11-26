using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace UniTrackBackend.Infrastructure
{
    public class YouTubeApiClient
    {
        private readonly YouTubeService _youtubeService;

        public YouTubeApiClient(string apiKey)
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "UniTrack"
            });
        }

        public YouTubeService GetService() => _youtubeService;
    }
}