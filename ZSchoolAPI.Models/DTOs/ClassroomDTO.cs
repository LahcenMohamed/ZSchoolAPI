using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models.DTOs
{
    public sealed class ClassroomDTO
    {
        public string? Id { get; set; }
        [Required]
        [Range(1,10_000)]
        public int ClassroomNumber { get; set; }
        [Required]
        [Range(6,50)]
        public int CountOfChairs { get; set; }
    }
}
