using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models.DTOs
{
    public sealed class SubjectDTO
    {
        public string? Id { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
