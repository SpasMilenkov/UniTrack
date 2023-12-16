using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Controllers;

/// <summary>
/// The MarkController is responsible for handling all HTTP requests related to the management of marks in the system.
/// It provides functionality to add, retrieve, update, and delete marks. Additionally, it supports fetching marks
/// based on various filters such as student ID, teacher ID, subject ID, and specific dates. The controller leverages
/// the IMarkService for business logic operations and IMapper for object mapping between view models and data models.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class MarkController : ControllerBase
{
    private readonly IMarkService _markService;
    private readonly IMapper _mapper;
    /// <summary>
    /// Initializes a new instance of the MarkController.
    /// </summary>
    /// <param name="markService">Service for managing marks.</param>
    /// <param name="mapper">Automapper for mapping between view models and data models.</param>
    public MarkController(IMarkService markService, IMapper mapper)
    {
        _markService = markService;
        _mapper = mapper;
    }

    /// <summary>
    /// Adds a new mark.
    /// </summary>
    /// <param name="model">The view model containing the mark details to add.</param>
    /// <returns>The created mark.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddMark([FromBody] MarkViewModel model)
    {
        if (!ModelState.IsValid || model is null)
            return BadRequest();
        
        var mark = _mapper.MapMark(model);
        if (mark is null)
            return BadRequest();
        var createdMark = await _markService.AddMarkAsync(mark);
        return CreatedAtAction(nameof(GetMark), new { id = createdMark.Id }, createdMark);
    }

    /// <summary>
    /// Retrieves a mark by its ID.
    /// </summary>
    /// <param name="id">The ID of the mark.</param>
    /// <returns>The requested mark.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMark(int id)
    {
        var mark = await _markService.GetMarkByIdAsync(id);
        if (mark is null)
            return NotFound();

        return Ok(mark);
    }

    /// <summary>
    /// Retrieves all marks.
    /// </summary>
    /// <returns>A list of all marks.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMarks()
    {
        var marks = await _markService.GetAllMarksAsync();
        return Ok(marks);
    } 
            
    /// <summary>
    /// Retrieves marks for a specific student.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>A list of marks for the specified student.</returns>
    [HttpGet("student/{studentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMarksByStudent(int studentId)
    {
        var marks = await _markService.GetMarksByStudentAsync(studentId);
        return Ok(marks);
    }

    /// <summary>
    /// Retrieves marks assigned by a specific teacher.
    /// </summary>
    /// <param name="teacherId">The ID of the teacher.</param>
    /// <returns>A list of marks assigned by the specified teacher.</returns>
    [HttpGet("teacher/{teacherId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMarksByTeacher(int teacherId)
    {
        var marks = await _markService.GetMarksByTeacherAsync(teacherId);
        return Ok(marks);
    }

    /// <summary>
    /// Retrieves marks for a specific subject.
    /// </summary>
    /// <param name="subjectId">The ID of the subject.</param>
    /// <returns>A list of marks for the specified subject.</returns>
    [HttpGet("subject/{subjectId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMarksBySubject(int subjectId)
    {
        var marks = await _markService.GetMarksBySubjectAsync(subjectId);
        return Ok(marks);
    }

    /// <summary>
    /// Retrieves marks assigned on a specific date.
    /// </summary>
    /// <param name="date">The date of interest.</param>
    /// <returns>A list of marks assigned on the specified date.</returns>
    [HttpGet("date/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMarksByDate(DateTime date)
    {
        var marks = await _markService.GetMarksByDateAsync(date);
        return Ok(marks);
    }


    /// <summary>
    /// Updates a specific mark.
    /// </summary>
    /// <param name="id">The ID of the mark to update.</param>
    /// <param name="model">The view model containing the updated mark details.</param>
    /// <returns>The updated mark.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMark(int id, [FromBody] MarkViewModel model)
    {
        if (id != model.Id)
            return BadRequest("ID mismatch");

        var mark = _mapper.MapMark(model);
        if (mark is null)
            return NotFound();
        var updatedMark = await _markService.UpdateMarkAsync(mark);
        
        if (updatedMark is null || updatedMark.Id != id)
            return NotFound();

        return Ok(updatedMark);
    }

    /// <summary>
    /// Deletes a specific mark.
    /// </summary>
    /// <param name="id">The ID of the mark to delete.</param>
    /// <returns>No content on successful deletion, NotFound if the mark does not exist.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMark(int id)
    {
        try
        {
            var result = await _markService.DeleteMarkAsync(id);
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