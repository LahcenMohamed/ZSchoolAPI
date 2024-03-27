using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZScool.Services.IServices;

namespace ZSchoolAPI.Teacher
{
    [Route("[controller]s")]
    [ApiController]
    public class TeacherUserController(ITeacherServices teacherServices) : ControllerBase
    {
        private readonly ITeacherServices _teacherServices = teacherServices;

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> index(string Id)
        {
            var teacher = await _teacherServices.GetById(Id);
            return Ok(teacher);
        }
    }
}
