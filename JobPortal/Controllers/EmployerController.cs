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

        //Add
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate(EmployerDto employerDto)
        {
            await _employerService.UpsertAsync(employerDto);
            return Ok(new { Message = "Created a employer", data = employerDto });

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


        
        //public async Task<IActionResult> UploadEmployerAsync([FromForm] EmployerDto employer)
        //{
        //    if (employer == null)
        //    {
        //        return BadRequest("Invalid employer  data.");
        //    }

        //    var result = await _employerService.AddAsync(employer);

        //    if (result == null)
        //        return StatusCode(500, "Failed to upload employer.");

        //    return Ok(new { message = "Employer  uploaded successfully!", employer = result });
        //}
        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        //{
        //    var filePath = await _employerService.AddAsync(file, "uploads");
        //    if (filePath == null) return BadRequest("File upload failed.");
        //    return Ok(new { filePath });
        //}


    }

}





