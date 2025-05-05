using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Claims;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly object _logger;

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

        [HttpGet("GetByUserId/{id}")]
        public async Task<ActionResult<EmployerDto>> GetByUserId(string id)
        {
            var employer = await _employerService.GetByUserIdAsync(Guid.Parse(id));

            if (employer == null)
                return NotFound("Employer not found.");

            return Ok(employer);
        }
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






    }

}





