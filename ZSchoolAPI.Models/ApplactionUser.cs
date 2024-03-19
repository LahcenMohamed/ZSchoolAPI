using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models
{
    public sealed class ApplactionUser : IdentityUser
    {
        public string? UserTypeId { get; set; }
        [AllowedValues("Admin","Teacher","Student")]
        public string UserType { get; set; }
    }
}
