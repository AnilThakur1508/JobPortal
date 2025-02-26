using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;

        public EmployerController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EmployerDto>>> GetAll()
        {
            var employers = await _employerService.GetAllAsync();
            return Ok(employers);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EmployerDto>> GetById(Guid id)
        {
            var employer = await _employerService.GetByIdAsync(id)
;
            if (employer == null)
                return NotFound("Employer not found.");

            return Ok(employer);
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployer(Guid id, [FromBody] EmployerDto employerDto)
        {
            await _employerService.UpdateAsync(id, employerDto);


            return Ok("Employer updated successfully.");
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployer(Guid id)
        {
            var success = await _employerService.DeleteAsync(id)
;
            if (!success)
                return NotFound("Employer not found.");

            return Ok("Employer deleted successfully.");
        }
        [HttpGet("GetFormattedAddressByUserId/{userId}")]
        public async Task<IActionResult> GetFormattedAddressByUserId(Guid userId)
        {
            var formattedAddress = await _employerService.GetFormattedAddressByUserId(userId);
            return Ok(new { Address = formattedAddress });
        }



        [HttpPost("upload-employer")]
        public async Task<IActionResult> UploadEmployer([FromForm] EmployerDto employer)
        {
            if (employer == null)
            {
                return BadRequest("Invalid employer  data.");
            }

            var result = await _employerService.AddAsync(employer);

            if (result == null)
                return StatusCode(500, "Failed to upload employer.");

            return Ok(new { message = "Employer  uploaded successfully!", employer = result });
        }

    }
}
    

