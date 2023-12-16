using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// Handles teacher-related actions such as managing teacher records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// Retrieves all teachers.
        /// </summary>
        /// <returns>A list of all teachers.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Teacher>))]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        /// <summary>
        /// Retrieves a specific teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to retrieve.</param>
        /// <returns>The teacher object if found, otherwise returns not found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Teacher))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        /// <summary>
        /// Adds a new teacher.
        /// </summary>
        /// <param name="teacher">The teacher object to add.</param>
        /// <returns>Returns the created teacher object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Teacher))]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            var createdTeacher = await _teacherService.AddTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacher), new { id = createdTeacher.Id }, createdTeacher);
        }

        /// <summary>
        /// Updates an existing teacher's details.
        /// </summary>
        /// <param name="id">The ID of the teacher to update.</param>
        /// <param name="teacher">The updated teacher object.</param>
        /// <returns>Returns no content if update is successful, otherwise a bad request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.Id)
                return BadRequest();

            await _teacherService.UpdateTeacherAsync(teacher);
            return NoContent();
        }

        /// <summary>
        /// Deletes a teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to delete.</param>
        /// <returns>Returns no content if deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return NoContent();
        }
    }
}
