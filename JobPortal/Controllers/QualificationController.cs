using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("api/Qualification")]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationService _service;
        public  QualificationController(IQualificationService qualificationService)
        {
            _service = qualificationService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<QualificationDto>>> GetAllAsync()
        {
           var qualification = await _service.GetAllAsync();
            return Ok(new { Meassage = "List of the qualification", Data =qualification });
        }
        
       
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<QualificationDto>> GetById(Guid id)
        {
            var qualification = await _service.GetByIdAsync(id);

            if (qualification == null)
                return NotFound("Qualication not found.");

            return Ok(qualification);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(QualificationDto qualificationDto)
        {
            await _service.AddAsync(qualificationDto);
            return Ok(new { Message = "Created a Qualification", data = qualificationDto });
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateQualification(Guid id, [FromBody] QualificationDto qualificationDto)
        {
            await _service.UpdateAsync(id, qualificationDto);


            return Ok("Qualification updated successfully.");
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var success = await _service.DeleteAsync(id)
;
            if (!success)
                return NotFound("Qualification not found.");

            return Ok("Qualification deleted successfully.");
        }
       


    }
}
    

