using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System.Security.Claims;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
     
    {
        private readonly IJobService _jobService;
        private readonly IRepository<Employer> _repository; 

        public JobController(IJobService jobService, IRepository<Employer> repository)
        {
            _jobService = jobService;
            _repository = repository; 
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetAllAsync()
        {
            var jobs = await _jobService.GetAllAsync();
            return Ok(new { Message = "List of the jobs", Data = jobs });
        }
        

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<JobDto>> GetById(Guid id)
        {
            var job = await _jobService.GetByIdAsync(id);

            if (job == null)
                return NotFound("Job not found.");

            return Ok(job);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] JobDto jobDto)
        {
            if (jobDto == null)
            {
                return BadRequest("Invalid job data.");
            }

            //// Get the User Id from JWT token claims
            //string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized("User is not logged in.");
            //}

            //// Fetch the Employer using UserId
            //var employer = await _repository.FirstOrDefaultAsync(e => e.UserId == Guid.Parse(userId));
            //if (employer == null)
            //{
            //    return NotFound("Employer not found.");
            //}

            //// Assign Employer Id to Job
            //jobDto.EmployerId = employer.Id;

            // Save Job
            var isAdded = await _jobService.AddAsync(jobDto);
            if (isAdded)
            {
                return Ok(new { Message = "Job created successfully", data = jobDto });
            }
            return BadRequest("Failed to create job.");
        }


        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] JobDto jobDto)
            {
            var success = await _jobService.UpdateAsync(id, jobDto);
            if (success)
            {
                return Ok(new { message = "Job updated successfully!" });
            }
            return BadRequest(new { error = "Failed to update job." });
        }



        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var success = await _jobService.DeleteAsync(id);

            if (!success)
                return NotFound("Job not found.");

            return Ok("Job deleted successfully.");
        }


    }
}
