using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services.AnalysisService;

namespace UniTrackBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IAnalysisService _analysisService;
    public AnalysisController(IAnalysisService analysisService)
    {
        _analysisService = analysisService;
    }

    // Example: GET api/sample
    [HttpGet]
    public async Task<IActionResult> GetAnalysis(int studentId)
    {
        var analysis = await _analysisService.GenerateAnalysisModel(studentId);
        if (analysis is null)
            return BadRequest();
        return Ok(analysis);
    }

    // Additional actions (POST, PUT, DELETE, etc.) can be added here
}