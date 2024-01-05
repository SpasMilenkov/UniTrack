using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Controllers;

/// <summary>
/// Handles teacher-related actions such as managing teacher records.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly IMapper _mapper;
    private readonly IGradeService _gradeService;
    
    public TeacherController(ITeacherService teacherService, IMapper mapper, IGradeService gradeService)
    {
        _mapper = mapper;
        _gradeService = gradeService;
        _teacherService = teacherService;
    }

    /// <summary>
    /// Retrieves all teachers.
    /// </summary>
    /// <returns>A list of all teachers.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherResultDto>))]
    public async Task<IActionResult> GetAllTeachers()
    {
        var teachers = await _teacherService.GetAllTeachersAsync();
        var models = new List<TeacherResultDto>();
        foreach (var teacher in teachers)
        {
            var gradeInfo = await _teacherService.GetGradeByClassTeacherId(teacher.Id);
            
            var model = _mapper.MapTeacherViewModel(teacher, gradeInfo.Id.ToString(), gradeInfo.Name);
            if (model is not null)
            {
                models.Add(model);
            }
        }
        return Ok(models);
    }

    /// <summary>
    /// Retrieves a specific teacher by their ID.
    /// </summary>
    /// <param name="id">The ID of the teacher to retrieve.</param>
    /// <returns>The teacher object if found, otherwise returns not found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherResultDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTeacher(int id)
    {
        var teacher = await _teacherService.GetTeacherByIdAsync(id);
        if (teacher == null)
            return NotFound();
        var gradeInfo = await _gradeService.GetClassTeacherData(teacher.Id);
        if (gradeInfo is not  null)
        {
            var classTeacherModel = _mapper.MapTeacherViewModel(teacher, gradeInfo.Value.Key, gradeInfo.Value.Value);
            return Ok(classTeacherModel);
        }
        var model = _mapper.MapTeacherViewModel(teacher);
        return Ok(model);
    }
    

    /// <summary>
    /// Updates an existing teacher's details.
    /// </summary>
    /// <param name="id">The ID of the teacher to update.</param>
    /// <param name="teacher">The updated teacher object.</param>
    /// <returns>Returns no content if update is successful, otherwise a bad request.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherDto teacher)
    {
        await _teacherService.UpdateTeacherAsync(teacher, id);
        return Ok("Teacher updated");
    }

    /// <summary>
    /// Deletes a teacher by their ID.
    /// </summary>
    /// <param name="id">The ID of the teacher to delete.</param>
    /// <returns>Returns no content if deletion is successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        await _teacherService.DeleteTeacherAsync(id);
        return Ok("Teacher deleted");
    }
}