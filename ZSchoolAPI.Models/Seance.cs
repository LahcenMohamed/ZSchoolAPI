using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models
{
    public sealed class Seance
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateOnly SeanceDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}
        [ForeignKey("Classroom")]
        public string ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
