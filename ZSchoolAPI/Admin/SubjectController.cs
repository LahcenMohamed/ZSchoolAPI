using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZSchoolAPI.Models.DTOs;
using ZScool.Services.IServices;

namespace ZSchoolAPI.Admin
{
    [Route("[controller]es")]
    [ApiController]
    public sealed class SubjectController(ISubjectServices _subjectServices) : ControllerBase
    {
        private readonly ISubjectServices subjectServices = _subjectServices;

        [HttpGet()]
        public async Task<ActionResult> GetAll()
        {
            var ls = await subjectServices.GetAllAsEnumerable();
            return Ok(ls);
        }

        [HttpPost()]
        public async Task<ActionResult> Create([FromForm]SubjectDTO subjectDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await subjectServices.Add(DTOMapper.SubjectDtoMapper(subjectDTO));
                    return Created();
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
            await subjectServices.Delete(Id);
            return Ok();
        }
    }
}
