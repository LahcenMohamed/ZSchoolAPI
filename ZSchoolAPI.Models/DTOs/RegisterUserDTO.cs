using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSchoolAPI.Models.DTOs
{
    public sealed class RegisterUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string UserTypeId { get; set; }
    }
}
