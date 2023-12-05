using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UniTrackBackend.Data.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Teacher> Teachers { get; set; } = null!;
        public ICollection<Student> Students { get; set; } = null!;
    }
}
