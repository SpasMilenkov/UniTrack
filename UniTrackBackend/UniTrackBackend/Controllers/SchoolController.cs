using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services.Mappings;
using UniTrackBackend.Services;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Api.DTO.ResultDtos;
using UniTrackBackend.Data.Models;

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

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<ActionResult<SchoolResultDto>> GetAllSchools()
        {
           var schools =  await _schoolService.GetAllSchoolsAsync();
           if (schools is null)
               return NotFound();
           return Ok(schools.Select(s => new SchoolResultDto(s.Name, s.Id.ToString())));
        }

    }
}
