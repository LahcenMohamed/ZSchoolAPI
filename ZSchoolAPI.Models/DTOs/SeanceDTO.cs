using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models.DTOs
{
    public sealed class SeanceDTO
    {
        public string? Id { get; set; } 
        public DateOnly SeanceDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string ClassroomId { get; set; }
        public string TeacherId { get; set; }
    }
}
