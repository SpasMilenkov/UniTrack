using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTrackBackend.Api.ViewModels
{
    public class AbsenceViewModel
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public decimal Value { get; set; } // Assuming Value represents some form of absence value or duration
        public DateTime Time { get; set; }
    }

   
}
