using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// The StudentController manages student-related operations within the system.
    /// It provides functionalities such as adding, retrieving, updating, and deleting student records.
    /// The controller interacts with the IStudentService to handle business logic and uses IMapper for model mapping.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the StudentController with the required services.
        /// </summary>
        /// <param name="studentService">Service for managing student data.</param>
        /// <param name="mapper">Service for mapping view models to entities.</param>
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>The student data if found; otherwise, a NotFound result.</returns>
        [HttpGet("studentId/{id}")]   
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();
            var result = _mapper.MapStudentDto(student);
            return Ok(result);
        }
        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetStudentByUserId(string userId)
        {
            var student = await _studentService.GetStudentByUserIdAsync(userId);
            if (student == null)
                return NotFound();
            var result = _mapper.MapStudentDto(student);
            return Ok(result);
        }
        /// <summary>
        /// Retrieves all student records.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// Deletes a student record by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A NoContent result indicating successful deletion, or NotFound if the student does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var result = await _studentService.DeleteStudentAsync(id);
                if (!result)
                    return BadRequest();
                return NoContent();
            }
            catch (System.Collections.Generic.KeyNotFoundException e)
            {
                return NotFound();
            }
        }
    }
}
