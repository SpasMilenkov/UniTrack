namespace UniTrackBackend.Data.Models;

public class Parent
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public List<Student> Children { get; set; } = null!;
}