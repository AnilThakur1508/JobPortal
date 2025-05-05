using DataAccessLayer.Entity;
using DataAccessLayer.PortalRepository;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
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
        public async Task<ActionResult<IEnumerable<JobResponseDto>>> GetAllAsync()
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
            var isAdded = await _jobService.AddAsync(jobDto);
            if (isAdded)
            {
                return Ok(new { Message = "Job created successfully", data = jobDto });
            }
            return BadRequest("Failed to create job.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JobDto jobDto)
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

        [HttpGet("job-counts-by-category")]
        public async Task<IActionResult> GetJobCountsByCategory()
        {
            try
            {
                var categoryCounts = await _jobService.GetJobCountsByCategoryAsync();

                if (categoryCounts == null || categoryCounts.Count == 0)
                    return NotFound("No job categories found.");

                return Ok(categoryCounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("Featured")]
        public async Task<IActionResult> GetFeaturedJobs()
        {
            var jobs = await _jobService.GetFeaturedJobsAsync();
            return Ok(jobs);
        }
        [HttpPost("JobFeaturedFilter")]
        public async Task<IActionResult> GetFeaturedJobs([FromBody] JobFilterDto? filters, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var (jobs, totalCount) = await _jobService.GetJobsAsync(filters, pageNumber, pageSize);
            return Ok(new
            {
                TotalCount = totalCount,
                Jobs = jobs
            });
        }
       
      
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetJobDetail(Guid id)
        {
            var jobDetail = await _jobService.GetJobDetailByIdAsync(id);

            if (jobDetail == null)
            {
                return NotFound(new { Message = "Job not found." });
            }

            return Ok(jobDetail);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchJobs([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Keyword is required.");

            var jobs = await _jobService.SearchJobsAsync(keyword);
            return Ok(jobs);
        }
    }
}
