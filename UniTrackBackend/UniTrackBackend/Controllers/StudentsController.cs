using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Services.StudentService;
using UniTrackBackend.Services.StudentService;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// The StudentController is responsible for managing student-related operations in the system.
    /// It provides functionality to retrieve and delete student records. The controller uses the IStudentService
    /// to interact with the underlying data layer and perform business logic operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        /// <summary>
        /// Initializes a new instance of the StudentController with the required services.
        /// </summary>
        /// <param name="studentService">Service for managing student data.</param>
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //
        // [HttpPost]
        // public async Task<IActionResult> AddStudent([FromBody] StudentViewModel model)
        // {
        //     var createdStudent = await _studentService.AddStudentAsync(student);
        //     return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
        // }

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

            return Ok(student);
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentViewModel model)
        // {
        //     if (id != model.)
        //         return BadRequest();
        //
        //     await _studentService.UpdateStudentAsync(student);
        //     return NoContent();
        // }
        
        /// <summary>
        /// Deletes a student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A NoContent result indicating successful deletion.</returns>
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
