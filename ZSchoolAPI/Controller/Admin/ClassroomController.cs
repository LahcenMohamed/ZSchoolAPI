using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZSchoolAPI.Models.DTOs;
using ZScool.Services.IServices;

namespace ZSchoolAPI.Admin
{
    [Route("/[controller]s")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public sealed class ClassroomController(IClassroomServices _classroomServices) : ControllerBase
    {
        private readonly IClassroomServices classroomServices = _classroomServices;

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ls = await classroomServices.GetAllAsEnumerable();
            return Ok(ls);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<ActionResult> Details(string Id)
        {
            var classRoom = await classroomServices.GetById(Id);
            return Ok(classRoom);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ClassroomDTO classroomDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await classroomServices.Add(DTOMapper.ClassroomDtoMapper(classroomDTO));
                    return Created();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState.ToList());
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromForm] ClassroomDTO classroomDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await classroomServices.Update(DTOMapper.ClassroomDtoMapper(classroomDTO));
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState.ToList());
        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> Delete(string Id)
        {
            try
            {
                await classroomServices.Delete(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not delete {Id}");
            }
        }
    }
}
