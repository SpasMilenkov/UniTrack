using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services;
using UniTrackBackend.Api.ViewModels;

namespace UniTrackBackend.Controllers
{
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        private readonly IMapper _mapper;
      
        public SchoolController(ISchoolService schoolService, IMapper mapper)
        {
            _schoolService = schoolService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSchool([FromBody] SchoolViewModel model)
        {
            var school = _mapper.MapSchool(model);
            if (school is null)
                return BadRequest();
            var createdSchool = await _schoolService.AddSchoolAsync(school);
            return CreatedAtAction(nameof(GetSchool), new { id = createdSchool.Id }, createdSchool);
        }
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSchools()
        {
            var schools = await _schoolService.GetAllSchoolsAsync();
            return Ok(schools);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] SchoolViewModel model)
        {
            if (id != model.Id)
                return BadRequest("ID mismatch");

            var school = _mapper.MapSchool(model);
            if (school is null)
                return NotFound();
            var updatedSchool = await _schoolService.UpdateSchoolAsync(school);

            return Ok(updatedSchool);
        }
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
