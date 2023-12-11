namespace UniTrackBackend.Data.Models;

public class Admin
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public int SchoolId { get; set; }
    public School School { get; set; } = null!;
}