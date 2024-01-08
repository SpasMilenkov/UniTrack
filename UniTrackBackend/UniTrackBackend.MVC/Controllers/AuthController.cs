using Microsoft.AspNetCore.Mvc;
using UniTrackBackend.Api.DTO;
using UniTrackBackend.Services;

namespace UniTrackBackend.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginDto model)
        {
            if (!ModelState.IsValid) return View(model);
            
            var user = await _authService.LoginUser(model);

            if (user is not null)
            {
                return RedirectToAction("Index", "Home");
            }
                
            ModelState.AddModelError("", "Invalid login attempt");

            return View(model);
        }
    }
}