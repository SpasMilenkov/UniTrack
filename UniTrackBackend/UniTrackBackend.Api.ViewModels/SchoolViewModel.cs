using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Api.ViewModels
{
    public class SchoolViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Teacher> Teachers { get; set; }
        public List<Student> Students { get; set; }
    }
}
