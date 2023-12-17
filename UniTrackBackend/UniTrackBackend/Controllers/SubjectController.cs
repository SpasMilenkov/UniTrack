using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Services;
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
        public async Task<ActionResult<IEnumerable<SubjectResultViewModel>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        // GET: api/subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectResultViewModel>> GetSubject(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // POST: api/subjects
        [HttpPost]
        public async Task<ActionResult<SubjectResultViewModel>> AddSubject(SubjectViewModel subject)
        {
            var entity = _mapper.MapSubject(subject);
            var addedSubject = await _subjectService.AddSubjectAsync(entity);
            return CreatedAtAction(nameof(GetSubject), new { id = addedSubject.Id }, addedSubject);
        }

        // PUT: api/subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, SubjectViewModel subject)
        {
            try
            {
                var entity = _mapper.MapSubject(subject);
                await _subjectService.UpdateSubjectAsync(entity);
            }
            catch
            {
                // Here, you might check if the subject doesn't exist and return NotFound.
                // Otherwise, return a generic error.
                return StatusCode(500, "An error occurred while updating the subject.");
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> AssignTeachersToSubject(SubjectViewModel subjectViewModel)
        {
            
            return Ok();
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