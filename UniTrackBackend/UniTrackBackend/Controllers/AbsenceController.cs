using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services.AbsenceService;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// Handles actions related to student absences.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsencesController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        /// <summary>
        /// Records a new absence.
        /// </summary>
        /// <param name="absence">The absence details to record.</param>
        /// <returns>The created absence record.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Absence>> PostAbsence(Absence absence)
        {
            var newAbsence = await _absenceService.AddAbsenceAsync(absence);
            return CreatedAtAction(nameof(GetAbsenceById), new { id = newAbsence.Id }, newAbsence);
        }

        /// <summary>
        /// Retrieves all recorded absences.
        /// </summary>
        /// <returns>A list of all absences.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Absence>>> GetAllAbsences()
        {
            var absences = await _absenceService.GetAbsencesAsync();
            return Ok(absences);
        }

        /// <summary>
        /// Retrieves a specific absence by its ID.
        /// </summary>
        /// <param name="id">The ID of the absence to retrieve.</param>
        /// <returns>The absence record if found, otherwise returns not found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Absence>> GetAbsenceById(int id)
        {
            var absence = await _absenceService.GetAbsencesByStudentIdAsync(id);
            if (absence == null) return NotFound();
            return Ok(absence);
        }

        /// <summary>
        /// Updates an existing absence record.
        /// </summary>
        /// <param name="id">The ID of the absence to update.</param>
        /// <param name="absence">The updated absence details.</param>
        /// <returns>Returns no content if the update is successful, otherwise a bad request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAbsence(int id, Absence absence)
        {
            if (id != absence.Id) return BadRequest("ID mismatch");
            await _absenceService.UpdateAbsenceAsync(absence);
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific absence record by its ID.
        /// </summary>
        /// <param name="id">The ID of the absence to delete.</param>
        /// <returns>Returns no content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAbsence(int id)
        {
            await _absenceService.DeleteAbsenceAsync(id);
            return NoContent();
        }
    }
}
