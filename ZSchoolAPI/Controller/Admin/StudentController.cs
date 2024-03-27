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
    public sealed class StudentController(IStudentServices _studentServices, IWebHostEnvironment _webHostEnvironment) : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment = _webHostEnvironment;
        private readonly IStudentServices studentServices = _studentServices;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ls = await studentServices.GetAllAsEnumerable();
            return Ok(ls);
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> Details(string Id)
        {
            var teacher = await studentServices.GetById(Id);
            return Ok(teacher);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] StudentDTO studentDTO, [FromForm] IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "Students");
                    string FileName = Guid.NewGuid().ToString() + "-" + formFile.FileName;
                    string file_path = Path.Combine(uploadsFolder, FileName);
                    using (var stream = new FileStream(file_path, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                        stream.Close();
                    }
                    studentDTO.ImageUrl = @"/Images/Students/" + FileName;
                    await studentServices.Add(DTOMapper.StudentDtoMapper(studentDTO));
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
        public async Task<ActionResult> Update([FromForm] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await studentServices.Update(DTOMapper.StudentDtoMapper(studentDTO));
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
                await studentServices.Delete(Id);
                return Created();

            }
            catch (Exception ex)
            {
                return BadRequest($"cannot delete {Id}");
            }

        }

    }
}
