using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Services;
using System.Threading.Tasks;

namespace UniTrackBackend.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: Notification/GetNotificationsForUser/{userId}
        [HttpGet("GetNotificationsForUser/{userId}")]
        public async Task<IActionResult> GetNotificationsForUser(int userId)
        {
            // Implement logic to retrieve notifications for the specified user
            // This could involve calling a method in the NotificationService
            // that interacts with the notification repository to fetch notifications.
            var notifications = await _notificationService.GetNotificationsForUserAsync(userId);
            if (notifications == null) return NotFound();
            return Ok(notifications);
        }

        // POST: Notification/SendNotifications
        [HttpPost("SendNotifications")]
        public async Task<IActionResult> SendNotifications()
        {
            // Trigger the sending of notifications.
            // This calls the SendNotificationsAsync method of NotificationService.
            await _notificationService.SendNotificationsAsync();
            return Ok("Notifications sent successfully.");
        }

        // Additional methods can be added here for other notification-related actions
        // such as marking notifications as read, deleting notifications, etc.
    }
}
