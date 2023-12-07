using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.TeacherService;

namespace UniTrackBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            var createdTeacher = await _teacherService.AddTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacher), new { id = createdTeacher.Id }, createdTeacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.Id)
                return BadRequest();

            await _teacherService.UpdateTeacherAsync(teacher);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return NoContent();
        }
    }
}
