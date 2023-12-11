using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services.RecommendationService;

namespace UniTrackBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly IRecommendationService _recommendationService;
    public RecommendationController(IRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    // Example: GET api/sample
    [HttpGet]
    public async Task<IActionResult> GetAnalysis(int studentId)
    {
        var result = await _recommendationService.GetRecommendations(studentId);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }
}