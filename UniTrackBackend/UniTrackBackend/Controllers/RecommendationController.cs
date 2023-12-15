using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services.RecommendationService;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// Handles the recommendation process for students.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        /// <summary>
        /// Retrieves recommendations based on a student's ID.
        /// </summary>
        /// <param name="studentId">The ID of the student for whom recommendations are being requested.</param>
        /// <returns>A list of recommendations if available, otherwise a bad request response.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAnalysis(int studentId)
        {
            var result = await _recommendationService.GetRecommendations(studentId);
            if (result is null)
                return BadRequest();
            return Ok(result);
        }
    }
}
