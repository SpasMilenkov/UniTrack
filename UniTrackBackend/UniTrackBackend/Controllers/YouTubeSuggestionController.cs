using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services;

namespace UniTrackBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorialSuggestionController : ControllerBase
    {
        private readonly IYouTubeSuggestionService _youtubeSuggestionService;

        public TutorialSuggestionController(IYouTubeSuggestionService youtubeSuggestionService)
        {
            _youtubeSuggestionService = youtubeSuggestionService;
        }

        [HttpGet("{subject}")]
        public async Task<IActionResult> GetSuggestions(string subject)
        {
            var suggestions = await _youtubeSuggestionService.GetYouTubeSuggestionsAsync(subject);
            return Ok(suggestions); // This will serialize the collection of YouTubeSuggestion objects to JSON
        }
    }
}
