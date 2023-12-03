using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTrackBackend.Data.Models
{
    public class School
    {
        public string Name { get; set; } = null!;
        public int Id { get; set; }
        public ICollection Teachers { get; set; } = null!;
        public ICollection Students { get; set; } = null!;
    }
}
