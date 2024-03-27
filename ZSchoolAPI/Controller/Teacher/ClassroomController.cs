using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZScool.Services.IServices;

namespace ZSchoolAPI.Teacher
{
    [Route("Teacher/[controller]s")]
    [ApiController]
    [Authorize("Teacher")]
    public class ClassroomController(IClassroomServices classroomServices) : ControllerBase
    {
        private readonly IClassroomServices _classroomServices = classroomServices;
        [HttpGet]
        public async Task<ActionResult> index()
         {
            var Id = User.FindFirst("UserTypeId")?.Value;
            var classrooms = await _classroomServices.GetClassroomsWithTeacherId(Id);
            return Ok(classrooms);
        }
    }
}
