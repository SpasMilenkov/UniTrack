using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Api.DTO
{
    public class SchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Teacher> Teachers { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = null!;
    }
}
