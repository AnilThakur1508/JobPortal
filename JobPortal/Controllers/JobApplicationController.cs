using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        
        private readonly IJobApplicationService _jobapplicationService;


        public JobApplicationController(IJobApplicationService jobapplicationService)
        {
            _jobapplicationService = jobapplicationService;
        }
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobApplicationDto>>> GetAllAsync()
        {
            var jobapplications = await _jobapplicationService.GetAllAsync();
            return Ok(new { Message = "List of the jobapplications", Data = jobapplications });
        }
        //GetbyId
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<JobApplicationDto>> GetById(Guid id)
        {
            var jobapplication = await _jobapplicationService.GetByIdAsync(id);

            if (jobapplication == null)
                return NotFound("JobApplication not found.");

            return Ok(jobapplication);
        } //Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(JobApplicationDto jobapplicationDto)
        {
            await _jobapplicationService.AddAsync(jobapplicationDto);
            return Ok(new { Message = "Created a jobapplication", data = jobapplicationDto });

        }
        //update
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateJobApplication(Guid id, [FromBody] JobApplicationDto jobapplicationDto)
        {
            await _jobapplicationService.UpdateAsync(id, jobapplicationDto);


            return Ok("JobApplication updated successfully.");
        }

        //delete

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteJobApplication(Guid id)
        {
            var success = await _jobapplicationService.DeleteAsync(id);

            if (!success)
                return NotFound("JobApplication not found.");

            return Ok("JobApplication deleted successfully.");
        }


    }
}

