using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;

namespace UniTrackBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await _adminService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            //try
            //{
            //    var userId = int.Parse(id); 
            //    var user = await _adminService.GetUserByIdAsync(userId);

            //    if (user == null)
            //    {
            //        return NotFound();
            //    }

            //    return Ok(user);
            //}
            //catch (FormatException)
            //{
            //    // Handle the case where the id is not a valid integer
            //    return BadRequest("User ID must be an integer.");
            //}
            try
            {
                var user = await _adminService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred processing your request.");
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("UpdateUser/{id}")]    
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest();

            await _adminService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _adminService.DeleteUserAsync(id);
            return NoContent();
        }

        // Implement other admin-specific endpoints here
    }

}
