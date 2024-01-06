using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// Controller for handling operations related to student and teacher absences.
    /// Provides functionality for recording, retrieving, updating, and deleting absence records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;
        private readonly IMapper _mapper;

        public AbsencesController(IAbsenceService absenceService, IMapper mapper)
        {
            _absenceService = absenceService;
            _mapper = mapper;
        }

        /// <summary>
        /// Records a new absence.
        /// </summary>
        /// <param name="absence">The absence details to be recorded.</param>
        /// <returns>An ActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAbsence(AbsenceDto absence)
        {
            var entity = _mapper.MapAbsence(absence);

            if (entity is null)
                return BadRequest("Invalid absence data");

            await _absenceService.AddAbsenceAsync(entity);
            return CreatedAtAction(nameof(GetAbsenceByStudentId), new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Retrieves all recorded absences.
        /// </summary>
        /// <returns>A list of all absence records.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AbsenceDto>>> GetAllAbsences()
        {
            var absences = await _absenceService.GetAbsencesAsync();
            var modelList = absences.Select(absence => _mapper.MapAbsenceResultDto(absence)).ToList();

            return Ok(modelList);
        }

        /// <summary>
        /// Retrieves absences for a specific student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student whose absences are to be retrieved.</param>
        /// <returns>An ActionResult containing the student's absence records or a not found result.</returns>
        [HttpGet("student/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAbsenceByStudentId(int id)
        {
            var absences = await _absenceService.GetAbsencesByStudentIdAsync(id);
            if (absences == null) return NotFound("Student not found");

            var models = absences.Select(absence => _mapper.MapAbsenceResultDto(absence)).ToList();

            return models.Any() ? Ok(models) : NotFound("No absences found for the specified student");
        }

        /// <summary>
        /// Retrieves absences for a specific teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher whose absences are to be retrieved.</param>
        /// <returns>An ActionResult containing the teacher's absence records or a not found result.</returns>
        [HttpGet("teacher/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAbsenceByTeacherId(int id)
        {
            var absences = await _absenceService.GetAbsencesByTeacherIdAsync(id);

            var models = absences.Select(absence => _mapper.MapAbsenceResultDto(absence)).ToList();

            return models.Any() ? Ok(models) : NotFound("No absences found for the specified teacher");
        }

        /// <summary>
        /// Updates an existing absence record.
        /// </summary>
        /// <param name="id">The ID of the absence record to update.</param>
        /// <param name="absence">The updated absence details.</param>
        /// <returns>Returns NoContent if the update is successful, otherwise returns BadRequest.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAbsence(int id, AbsenceDto absence)
        {
            var entity = _mapper.MapAbsence(absence);
            if (entity is null)
                return BadRequest("Invalid absence data");

            entity.Id = id;
            await _absenceService.UpdateAbsenceAsync(entity);
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific absence record by its ID.
        /// </summary>
        /// <param name="id">The ID of the absence record to delete.</param>
        /// <returns>Returns NoContent if the deletion is successful, otherwise NotFound.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAbsence(int id)
        {
            await _absenceService.DeleteAbsenceAsync(id);
            return NoContent();
        }
    }
}