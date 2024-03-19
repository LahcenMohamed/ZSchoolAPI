using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models
{
    public sealed class Student
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
        [ForeignKey("Classroom")]
        public string ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}
