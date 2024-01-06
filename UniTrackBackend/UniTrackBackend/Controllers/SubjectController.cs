using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services.SubjectService;

namespace UniTrackBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;
        public SubjectsController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        // GET: api/subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectResultDto>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            
            return Ok(subjects.Select(s => _mapper.MapSubjectResultDto(s)));
        }

        // GET: api/subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectResultDto>> GetSubject(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            
            if (subject == null)
            {
                return NotFound();
            }

            var response = _mapper.MapSubjectResultDto(subject);
            return Ok(response);
        }

        // POST: api/subjects
        [HttpPost]
        public async Task<ActionResult<SubjectResultDto>> AddSubject(SubjectDto subject)
        {
            var addedSubject = await _subjectService.AddSubjectAsync(subject);
            var response = _mapper.MapSubjectResultDto(addedSubject);
            if (response is null)
                return new StatusCodeResult(500);
            return CreatedAtAction(nameof(GetSubject), new { id = response.Id }, response);
        }

        // PUT: api/subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectDto subject)
        {
            try
            {
                await _subjectService.UpdateSubjectAsync(id, subject);
            }
            catch
            {
                // Here, you might check if the subject doesn't exist and return NotFound.
                // Otherwise, return a generic error.
                return StatusCode(500, "An error occurred while updating the subject.");
            }

            return Ok("Subject updated!");
        }
        // DELETE: api/subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(id);
            }
            catch
            {
                // Handle the error appropriately - for instance, return NotFound if the subject doesn't exist
                return StatusCode(500, "An error occurred while deleting the subject.");
            }

            return NoContent();
        }
    }
}