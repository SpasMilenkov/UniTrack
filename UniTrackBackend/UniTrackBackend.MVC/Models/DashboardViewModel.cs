namespace UniTrackBackend.MVC.Models;

public class DashboardViewModel
{ 
    public required CardViewModel SchoolCard { get; set; }
    public required CardViewModel TeacherCard { get; set; }
    public required CardViewModel StudentCard { get; set; }
    public required CardViewModel UserCard { get; set; }
    public required DashboardFormViewModel Form { get; set; }
}