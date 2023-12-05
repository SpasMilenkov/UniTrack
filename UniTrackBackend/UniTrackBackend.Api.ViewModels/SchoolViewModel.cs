using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Api.ViewModels
{
    public class SchoolViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Teacher> Teachers { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = null!;
    }
}
