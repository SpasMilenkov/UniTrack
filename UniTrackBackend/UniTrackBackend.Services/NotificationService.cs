using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data;
using UniTrackBackend.Data.Commons;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services
{
    public class NotificationService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IRepository<Notification> _notificationRepository; 

        public NotificationService(UnitOfWork unitOfWork, IRepository<Notification> notificationRepository)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;
        }
        private EventViewModel ConvertScheduleEventToViewModel(ScheduleEvent scheduleEvent)
        {
            return new EventViewModel
            {
                // Map the properties from ScheduleEvent to EventViewModel
                EventId = scheduleEvent.ScheduleEventId, // Assuming there is an EventId
                Name = scheduleEvent.EventName,
                Description = scheduleEvent.EventDescription,
                Date = scheduleEvent.StartTime,
                Location = scheduleEvent.Location
                
            };
        }
        public async Task SendNotificationsAsync()
        {
            var schedules = await _unitOfWork.ScheduleRepository.GetAllAsync();
            
            foreach (var schedule in schedules)
            {
                foreach (var scheduleEvent in schedule.Events) // This assumes schedule has a collection of ScheduleEvent
                {
                    var upcomingEventViewModel = ConvertScheduleEventToViewModel(scheduleEvent);

                    if (IsUserAvailableForEvent(schedule, upcomingEventViewModel))
                    {
                        await NotifyUser(schedule.UserId, upcomingEventViewModel);
                    }
                }
            }
        }
        public async Task<IEnumerable<NotificationViewModel>> GetNotificationsForUserAsync(int userId)
        {
            var notifications = await _notificationRepository.GetAsync(
                filter: n => n.UserId == userId,
                orderBy: q => q.OrderByDescending(n => n.DateCreated));

            return notifications.Select(n => new NotificationViewModel
            {
                // Map the properties from Notification to NotificationViewModel
                NotificationId = n.NotificationId,
                UserId = n.UserId,
                EventId = n.EventId,
                Message = n.Message,
                DateCreated = n.DateCreated,
                IsRead = n.IsRead
            }).ToList();
        }


        private bool IsUserAvailableForEvent(UserSchedule schedule, EventViewModel upcomingEvent)
        {
            return !schedule.Events.Any(se => se.StartTime <= upcomingEvent.Date && se.EndTime >= upcomingEvent.Date);
        }

        private async Task NotifyUser(int userId, EventViewModel upcomingEvent)
        {
            
            var notification = new Notification
            {
                UserId = userId,
                EventId = upcomingEvent.EventId,
                Message = $"You have an upcoming event: {upcomingEvent.Name}",
                DateCreated = DateTime.Now,
                IsRead = false
            };

            // Add the notification to the database using a repository
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.NotificationRepository.UpdateAsync(notification);
        }

    }

}
