using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services;
using UniTrackBackend.Api.DTO;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// Handles school-related actions such as managing school records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;

        public SchoolController(ISchoolService schoolService, IMapper mapper)
        {
            _schoolService = schoolService;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new school.
        /// </summary>
        /// <param name="model">The school view model to create a new school.</param>
        /// <returns>Returns the created school object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSchool([FromBody] SchoolDto model)
        {
            var school = _mapper.MapSchool(model);
            if (school is null)
                return BadRequest();
            var createdSchool = await _schoolService.AddSchoolAsync(school);
            return CreatedAtAction(nameof(GetSchool), new { id = createdSchool.Id }, createdSchool);
        }

        /// <summary>
        /// Retrieves a specific school by its ID.
        /// </summary>
        /// <param name="id">The ID of the school to retrieve.</param>
        /// <returns>The school object if found, otherwise returns not found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSchool(int id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);
            if (school is null)
                return NotFound();

            return Ok(school);
        }

        /// <summary>
        /// Retrieves all schools.
        /// </summary>
        /// <returns>A list of all schools.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchoolsAsync();
            return Ok(schools);
        }

        /// <summary>
        /// Updates the details of an existing school.
        /// </summary>
        /// <param name="id">The ID of the school to update.</param>
        /// <param name="model">The updated school view model.</param>
        /// <returns>Returns updated school object if successful, otherwise a bad request or not found.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] SchoolDto model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch");

            var school = _mapper.MapSchool(model);
            if (school is null)
                return NotFound();
            var updatedSchool = await _schoolService.UpdateSchoolAsync(school);

            return Ok(updatedSchool);
        }

        /// <summary>
        /// Deletes a school by its ID.
        /// </summary>
        /// <param name="id">The ID of the school to delete.</param>
        /// <returns>Returns no content if deletion is successful, otherwise not found.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            try
            {
                var result = await _schoolService.DeleteSchoolAsync(id);
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
