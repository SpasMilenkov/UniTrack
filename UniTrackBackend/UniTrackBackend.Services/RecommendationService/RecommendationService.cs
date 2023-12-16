using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services;

public class RecommendationService : IRecommendationService
{
    private readonly ILogger<RecommendationService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly Dictionary<string, IEnumerable<string>> _subjectToChannel;
    private readonly YouTubeService _youTubeService;
    
    public RecommendationService(ILogger<RecommendationService> logger, IConfiguration config, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;

        _subjectToChannel = new Dictionary<string, IEnumerable<string>>()
        {
            { "Mathematics", new List<string> { "UC4a-Gbdw7vOaccHmFo40b9g", "UCoxcjq-8xIDTYp3uz647V5A", "UCEWpbFLzoYGPfuWUMFPSaoA"} },
            { "Science", new List<string> { "UCX6b17PVsYBQ0ip5gyeme-Q", "UCZYTClx2T1of7BRZ86-8fow", "UCEik-U3T6u6JA0XiHLbNbOw", "UCEWpbFLzoYGPfuWUMFPSaoA"} },
            { "Language Arts", new List<string> { "UCsooa4yRKGN_zEE8iknghZA", "UC7IcJI8PUf5Z3zKxnZvTBog" } },
            { "Social Studies", new List<string> { "UCX6b17PVsYBQ0ip5gyeme-Q", "UC2C_jShtL725hvbm1arSV9w", "UCZ4AMrDcNrfy3X6nsU8-rPg" } },
            { "Foreign Languages", new List<string> { "UCG98ruDeyp55THxbpBCIv3g", "UCqcBu0YyEJH4vfKR--97cng" } },
            { "Art and Music", new List<string> { "UClM2LuQ1q5WEc23462tQzBg", "UCnkp4xDOwqqJD7sSM3xdUiQ" } },
            { "Physical Education and Health", new List<string> { "UCAxW1XT0iEJo0TYlRfn6rYQ", "UCddn8dUxYdgJz3Qr5mjADtA" } },
            { "Computer Science and Technology", new List<string> { "UCJyEBMU1xVP2be1-AoGS1BA", "UC8butISFwT-Wl7EV0hUK0BQ", "UC9-y-6csu5WGm29I7JiwpnA" } },
            { "Electives", new List<string> { "UCX6b17PVsYBQ0ip5gyeme-Q", "UCUdettijNYvLAm4AixZv4RA", "UC4EY_qnSeAP1xGsh61eOoJA" } }
        };
        
        _youTubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = config["YouTubeCredentials:ApiKey"],
            ApplicationName = "UniTrack"
        });
    }

    public async Task<IEnumerable<RecommendationResultViewModel>?> GetRecommendations(int studentId)
    {
        var student = await _unitOfWork.StudentRepository.FirstOrDefaultAsync(s => s.Id == studentId);
        if (student is null)
            return null;
        await _unitOfWork.StudentRepository.LoadCollectionAsync<Mark>(student, s => student.Marks);
        var studentMarks = student.Marks;

        var weaknesses = studentMarks.Where(m => m.Value <= 5);

        var queryList = new Dictionary<string, string>();
        foreach (var weakMark in weaknesses)
        {
            await _unitOfWork.MarkRepository.LoadReferenceAsync(weakMark, w => w.Subject);
            queryList.TryAdd(weakMark.Topic, weakMark.Subject.Name);
        }

        var searchTasks = new List<Task<RecommendationResultViewModel?>>();
        foreach (var query in queryList)
        {
            foreach (var token in _subjectToChannel[query.Value])
            {
                searchTasks.Add(PerformYouTubeSearch(query.Key, token));
            }
        }

        var searchResults = await Task.WhenAll(searchTasks);
        var results = searchResults.Where(r => r != null).ToList();

        return results;
    }


    private async Task<RecommendationResultViewModel?> PerformYouTubeSearch(string topic, string token)
    {
        
        var searchRequest = _youTubeService.Search.List("snippet");
        searchRequest.Order = SearchResource.ListRequest.OrderEnum.Rating;
        searchRequest.Q = topic;
        searchRequest.ChannelId = token;

        var searchResponse = await searchRequest.ExecuteAsync();
        var recommendation = searchResponse.Items.Select(i => new RecommendationResultViewModel
        {
            Title = i.Snippet.Title,
            Link = $"https://www.youtube.com/watch?v={i.Id.VideoId}",
            Thumbnail = i.Snippet.Thumbnails.Medium.Url,
            PublishedDate = i.Snippet.PublishedAtDateTimeOffset
        }).MaxBy(video => video.PublishedDate);
        
        return recommendation ?? null;
    }
}