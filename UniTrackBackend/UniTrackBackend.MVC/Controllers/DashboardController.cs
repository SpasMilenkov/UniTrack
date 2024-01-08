using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Data.Models;
using UniTrackBackend.MVC.Models;
using UniTrackBackend.MVC.Services;
using UniTrackBackend.Services;
using UniTrackBackend.Services.Mappings;

namespace UniTrackBackend.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly ISchoolService _schoolService;
        public DashboardController(
            IDashboardService dashboardService,
            ISchoolService schoolService)
        {
            _dashboardService = dashboardService;
            _schoolService = schoolService;
        }
        public async Task<IActionResult> Index()
        {
            var dashboardViewModel = await _dashboardService.GetDashboardData();
            return View(dashboardViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DashboardFormViewModel formModel)
        {
            if (ModelState.IsValid)
            {
                await _schoolService.AddSchoolAsync(formModel.SchoolName);
                // Redirect to a confirmation page or back to the dashboard
                return RedirectToAction("Index");
            }
            var dashboardViewModel = await _dashboardService.GetDashboardData();

            return View("Index", dashboardViewModel);
        }
    }
}