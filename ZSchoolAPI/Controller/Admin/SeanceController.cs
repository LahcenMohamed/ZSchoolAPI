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
    public sealed class SeanceController(ISeanceServices _seanceServices) : ControllerBase
    {
        private readonly ISeanceServices seanceServices = _seanceServices;
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ls = await seanceServices.GetAllAsEnumerable();
            return Ok(ls);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] SeanceDTO seanceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await seanceServices.Add(DTOMapper.SeanceDtoMapper(seanceDTO));
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
        public async Task<ActionResult> Update([FromForm] SeanceDTO seanceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await seanceServices.Update(DTOMapper.SeanceDtoMapper(seanceDTO));
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
                await seanceServices.Delete(Id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("cannot delete");
            }
        }

    }
}
