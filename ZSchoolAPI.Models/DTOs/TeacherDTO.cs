using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models.DTOs
{
    public sealed class TeacherDTO
    {
        public string? Id { get; set; } 
        [Required]
        [MaxLength(55)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(55)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [Range(0.0, double.MaxValue)]
        public decimal Salary { get; set; }
        public string? ImageUrl { get; set; }
        public string SubjectId { get; set; }
    }
}
