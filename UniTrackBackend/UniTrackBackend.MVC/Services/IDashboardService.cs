using UniTrackBackend.MVC.Models;

namespace UniTrackBackend.MVC.Services;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardData();
}