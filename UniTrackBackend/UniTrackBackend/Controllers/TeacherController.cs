using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService,IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher is null)
                return NotFound();

            return Ok(teacher);
        }

      
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTeacher([FromBody] TeacherViewModel model)
        {
            var teacher = _mapper.MapTeacher(model);
            if (teacher is null)
                return BadRequest();
            var createdTeacher = await _teacherService.AddTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacher), new { id = createdTeacher.Id }, createdTeacher);
        }


       
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] TeacherViewModel model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch");

            var teacher = _mapper.MapTeacher(model);
            if (teacher is null)
                return NotFound();
            var updatedTeacher = await _teacherService.UpdateTeacherAsync(teacher);

            return Ok(updatedTeacher);
        }

        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var result = await _teacherService.DeleteTeacherAsync(id);
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
}
