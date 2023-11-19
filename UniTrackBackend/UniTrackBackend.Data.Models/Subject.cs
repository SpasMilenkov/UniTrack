namespace UniTrackBackend.Data.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Teacher> Teachers { get; set; } = null!;
}