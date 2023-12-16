using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
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
        /// Adds a new student record to the system.
        /// </summary>
         /// <returns>The created student record.</returns>
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentViewModel model)
        {
            var student = _mapper.MapStudent(model);
            var createdStudent = await _studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        }

        /// <summary>
        /// Retrieves a student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>The student data if found; otherwise, a NotFound result.</returns>
        [HttpGet("{id}")]   
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();
            var result = _mapper.MapStudentViewModel(student);
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
        /// Updates an existing student record.
        /// </summary>
        /// <param name="id">The ID of the student to update.</param>
        /// <param name="model">The updated student view model.</param>
        /// <returns>A NoContent result indicating successful update.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentViewModel model)
        {
            if (id != model.StudentId)
                return BadRequest();

            var student = _mapper.MapStudent(model);
            await _studentService.UpdateStudentAsync(student);
            return NoContent();
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
