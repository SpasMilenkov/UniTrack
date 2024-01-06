using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// Handles analysis of student data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AnalysisController : ControllerBase
    {
        private readonly IAnalysisService _analysisService;

        public AnalysisController(IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        /// <summary>
        /// Generates an analysis for a given student based on their ID.
        /// </summary>
        /// <param name="studentId">The ID of the student for whom the analysis is to be generated.</param>
        /// <returns>An analysis model if successful, otherwise a bad request response.</returns>
        [HttpGet("ByStudentId/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAnalysis(int studentId)
        {
            var analysis = await _analysisService.GenerateAnalysisModel(studentId);
            if (analysis is null)
                return BadRequest();
            return Ok(analysis);
        }

        // Additional actions (POST, PUT, DELETE, etc.) can be added here with similar documentation.
    }
}
