using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Services.AbsenceService;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class AbsencesController : ControllerBase
{
    private readonly IAbsenceService _absenceService;
    private readonly IMapper _mapper;

    public AbsencesController(IAbsenceService absenceService, IMapper mapper)
    {
        _absenceService = absenceService;
        _mapper = mapper;   
    }

    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAbsence([FromBody] AbsenceViewModel model)
    {
        var absence = _mapper.MapAbsence(model);
        if (absence is null)
            return BadRequest();
        var createdAbsence = await _absenceService.AddAbsenceAsync(absence);
        return CreatedAtAction(nameof(GetAbsence), new { id = createdAbsence.Id }, createdAbsence);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAbsences()
    {
        var absences = await _absenceService.GetAbsencesAsync();   
        return Ok(absences);
    }

      
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAbsence(int id)
    {
        var absence = await _absenceService.GetAbsencesByStudentIdAsync(id);
        if (absence is null)
            return NotFound();

        return Ok(absence);
    }

      
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAbsence(int id, [FromBody] AbsenceViewModel model)
    {
        if (id != model.StudentId)
            return BadRequest("ID mismatch");

        var absence = _mapper.MapAbsence(model);
        if (absence is null)
            return NotFound();
        var updatedAbsence = await _absenceService.UpdateAbsenceAsync(absence);

        return Ok(updatedAbsence);
    }

      
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAbsence(int id)
    {
        try
        {
            var result = await _absenceService.DeleteAbsenceAsync(id);
            if (!result)
            {
                return NotFound();
            }
        }
        catch (KeyNotFoundException e)
        {
            return NotFound();
        }

        return Ok();
    }
}