using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Data.Models;
using UniTrackBackend.Services;

namespace UniTrackBackend.Controllers
{
    /// <summary>
    /// Handles administrative actions such as managing users.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>Returns a success message if creation is successful, otherwise a bad request.</returns>
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var result = await _adminService.CreateUserAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok("User created successfully");
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user object if found, otherwise returns not found.</returns>
        [HttpGet("GetUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Retrieves all registered users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public Task<IActionResult> GetAllUsers()
        {
            var users = _adminService.GetAllUsers();
            return Task.FromResult<IActionResult>(Ok(users));
        }

        /// <summary>
        /// Updates the details of an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>Returns no content if update is successful, otherwise a bad request.</returns>
        [HttpPut("UpdateUser/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest();

            await _adminService.UpdateUserAsync(user);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns no content if deletion is successful.</returns>
        [HttpDelete("DeleteUser/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _adminService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
