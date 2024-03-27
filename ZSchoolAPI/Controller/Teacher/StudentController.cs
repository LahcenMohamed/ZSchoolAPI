using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZScool.Services.IServices;

namespace ZSchoolAPI.Controller.Teacher
{
    [Route("Teacher/[controller]s")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class StudentController(IStudentServices studentServices) : ControllerBase
    {
        private readonly IStudentServices _studentServices = studentServices;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var Id = User.FindFirst("UserTypeId")?.Value;
            var lst = await _studentServices.GetAllWithTeacherId(Id);
            return Ok(lst);
        }
    }
}
