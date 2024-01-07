using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Services;


namespace UniTrackBackend.Controllers;


[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;
    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }
    
    // [HtppPost]
    // public async Task<ActionResult<GradeResultDto>> AddGrade(GradeDto grade)
    // {
    //     
    // }
    
    [HttpGet("getSubjectsBySchoolId/{schoolId}")]
    public async Task<ActionResult<IEnumerable<GradeResultDto>>> GetGradesBySchoolId(string schoolId)
    {
        var result = await _gradeService.GetAllGradesBySchoolId(int.Parse(schoolId));
       return Ok(result) ;
    }
}