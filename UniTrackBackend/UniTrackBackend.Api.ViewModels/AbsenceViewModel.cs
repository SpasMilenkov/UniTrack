using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTrackBackend.Api.ViewModels
{
    public class AbsenceViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AbsenceCount { get; set; }
        
        public bool Excused { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int StudentId {  get; set; } 

        [Required]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TeacherFirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string TeacherLastName { get; set; }
    }


}
