using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZSchoolAPI.Models.DTOs;
using ZScool.Services.IServices;

namespace ZSchoolAPI.Admin
{
    [Route("[controller]s")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public sealed class TeacherController(ITeacherServices _teacherServices,IWebHostEnvironment _webHostEnvironment) : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment = _webHostEnvironment;
        private readonly ITeacherServices teacherServices = _teacherServices;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ls = await teacherServices.GetAllAsEnumerable();
            return Ok(ls);
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> Details(string Id)
        {
            var teacher = await teacherServices.GetById(Id);
            return Ok(teacher);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromForm] TeacherDTO teacherDTO, [FromForm] IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "Teachers");
                    string FileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
                    string file_path = Path.Combine(uploadsFolder, FileName);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                        stream.Close();
                    }
                    teacherDTO.ImageUrl = @"/Images/Teachers/" + FileName;
                    await teacherServices.Add(DTOMapper.TeacherDtoMapper(teacherDTO));
                    return Created();

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState.FirstOrDefault());
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] TeacherDTO teacherDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {  
                    var url = await teacherServices.GetById(teacherDTO.Id);
                    teacherDTO.ImageUrl = url.ImageUrl;
                    await teacherServices.Update(DTOMapper.TeacherDtoMapper(teacherDTO));
                    return Created();

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState.FirstOrDefault());
        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> Delete(string Id)
        {

            try
            {
                await teacherServices.Delete(Id);
                return Created();

            }
            catch (Exception ex)
            {
                return BadRequest($"cannot delete {Id}");
            }

        }

    }
}
