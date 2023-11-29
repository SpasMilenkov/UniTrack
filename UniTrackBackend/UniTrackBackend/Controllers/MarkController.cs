using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkController : ControllerBase
    {
        private readonly IMarkService _markService;

        public MarkController(IMarkService markService)
        {
            _markService = markService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMark([FromBody] Mark mark)
        {
            
            mark.GradedOn = DateTime.Now; 

            var createdMark = await _markService.AddMarkAsync(mark);
            return CreatedAtAction(nameof(GetMark), new { id = createdMark.Id }, createdMark);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMark(int id)
        {
            var mark = await _markService.GetMarkByIdAsync(id);
            if (mark == null)
                return NotFound();

            return Ok(mark);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            var marks = await _markService.GetAllMarksAsync();
            return Ok(marks);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetMarksByStudent(int studentId)
        {
            var marks = await _markService.GetMarksByStudentAsync(studentId);
            return Ok(marks);
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetMarksByTeacher(int teacherId)
        {
            var marks = await _markService.GetMarksByTeacherAsync(teacherId);
            return Ok(marks);
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetMarksBySubject(int subjectId)
        {
            var marks = await _markService.GetMarksBySubjectAsync(subjectId);
            return Ok(marks);
        }

        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetMarksByDate(DateTime date)
        {
            var marks = await _markService.GetMarksByDateAsync(date);
            return Ok(marks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMark(int id, [FromBody] Mark mark)
        {
            if (id != mark.Id)
                return BadRequest("ID mismatch");

            var updatedMark = await _markService.UpdateMarkAsync(mark);
            return Ok(updatedMark);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMark(int id)
        {
            await _markService.DeleteMarkAsync(id);
            return NoContent();
        }

        // Additional endpoints can be added as needed
    }
}
