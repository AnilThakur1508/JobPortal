using DataAccessLayer.Entity;
using DataAccessLayer.Enum;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _jobApplicationService.GetAllAsync();
            return Ok(applications);
        }
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForJob([FromForm] JobApplicationDto dto)
        {
            try
            {
                await _jobApplicationService.ApplyForJobAsync(dto);
                return Ok(new { message = "Application submitted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{Id}/Status")]
        public async Task<ActionResult<bool>> UpdateStatusAsync(Guid Id,[FromBody] AppStatus appStatus)
        {
            var success = await _jobApplicationService.UpdateStatusAsync(Id, appStatus);
            if (!success) 

                return NotFound();

            return NoContent();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<JobApplicationResponseDto>> GetByIdAsync(Guid Id)
        {
            var application = await _jobApplicationService.GetByIdAsync(Id);
            if (application == null)
                return NotFound("JobApplication not found.");

            return Ok(application);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] JobApplicationDto dto)
        {
            var result = await _jobApplicationService.UpdateAsync(Id, dto);
            if (result)
            {
                return Ok(new { message = "Job updated successfully!" });
            }
            return BadRequest(new { error = "Failed to update jobapplication." });
        }

    }

}

