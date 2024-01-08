using Microsoft.AspNetCore.Identity;
using UniTrackBackend.Data.Models;
using UniTrackBackend.MVC.Models;
using UniTrackBackend.Services;

namespace UniTrackBackend.MVC.Services;

public class DashboardService : IDashboardService
{
    private readonly ISchoolService _schoolService;
    private readonly ITeacherService _teacherService;
    private readonly IStudentService _studentService;
    private readonly UserManager<User> _userService;
    public DashboardService(
        ISchoolService schoolService,
        ITeacherService teacherService,
        IStudentService studentService,
        UserManager<User> userService
    )
    {
        _schoolService = schoolService;
        _teacherService = teacherService;
        _studentService = studentService;
        _userService = userService;
    }
    public async Task<DashboardViewModel> GetDashboardData()
    {
        var schools = await _schoolService.GetAllSchoolsAsync() ?? new List<School>();
        var schoolCountCard = new CardViewModel()
        {
            Title = "Total schools in the system:",
            Content = schools.Count()
        };
        var students = await _studentService.GetAllStudentsAsync();
        var studentCountCard = new CardViewModel()
        {
            Title = "Total students in the system",
            Content = students.Count()
        };
        var teachers = await _teacherService.GetAllTeachersAsync();
        var teacherCountCard = new CardViewModel()
        {
            Title = "Total teachers in the system",
            Content = teachers.Count()
        };
        var usersCount = _userService.Users.Count();
        var userCountCard = new CardViewModel()
        {
            Title = "Total users in the system",
            Content = usersCount
        };
        var dashboardViewModel = new DashboardViewModel
        {
            Form = new DashboardFormViewModel(),
            SchoolCard = schoolCountCard,
            TeacherCard = teacherCountCard,
            StudentCard = studentCountCard,
            UserCard = userCountCard
        };
        return dashboardViewModel;
    }
}