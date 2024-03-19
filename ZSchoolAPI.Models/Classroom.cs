using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models
{
    public sealed class Classroom
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int ClassroomNumber { get; set; }
        public int CountOfChairs { get; set; }
        public List<Seance> Seances { get; set; }
        public List<Student> Students { get; set; }
    }
}
