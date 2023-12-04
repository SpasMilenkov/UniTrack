using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.AbsenceService;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;
        private readonly IMapper _mapper;

        public AbsencesController(IMapper mapper,IAbsenceService absenceService)
        {
            _mapper = mapper;
            _absenceService = absenceService;
        }


        [HttpPost]
        public async Task<ActionResult<Absence>> PostAbsence(Absence absence)
        {
            var newAbsence = await _absenceService.AddAbsenceAsync(absence);
            return CreatedAtAction(nameof(GetAbsenceById), new { id = newAbsence.Id }, newAbsence);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbsenceViewModel>>> GetAllAbsences()
        {
            var absences = await _absenceService.GetAbsencesAsync();
            var models = new List<AbsenceViewModel>();
            foreach (var absence in absences)
            {
                models.Add(_mapper.MapAbsenceViewModel(absence));
            }
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AbsenceViewModel>> GetAbsenceById(int id)
        {
            var absences = await _absenceService.GetAbsencesByStudentIdAsync(id);
            var models = new List<AbsenceViewModel>();
            if (absences == null) return NotFound();
            foreach (var absence in absences)
            {
                models.Add(_mapper.MapAbsenceViewModel(absence));
            }
            return Ok(models);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbsence(int id, Absence absence)
        {
            if (id != absence.Id) return BadRequest("ID mismatch");
            await _absenceService.UpdateAbsenceAsync(absence);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbsence(int id)
        {
            await _absenceService.DeleteAbsenceAsync(id);
            return NoContent();
        }
    }
}
