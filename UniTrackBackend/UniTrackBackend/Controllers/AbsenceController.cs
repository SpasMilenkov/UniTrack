using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Data.Interfaces;
using UniTrackBackend.Data.Models;


namespace UniTrackBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsencesController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        [HttpPost]
        public async Task<ActionResult<Absence>> PostAbsence(Absence absence)
        {
            var newAbsence = await _absenceService.AddAbsenceAsync(absence);
            return CreatedAtAction(nameof(GetAbsenceById), new { id = newAbsence.Id }, newAbsence);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Absence>>> GetAllAbsences()
        {
            var absences = await _absenceService.GetAbsencesAsync();
            return Ok(absences);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Absence>> GetAbsenceById(int id)
        {
            var absence = await _absenceService.GetAbsencesByStudentIdAsync(id);
            if (absence == null) return NotFound();
            return Ok(absence);
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
