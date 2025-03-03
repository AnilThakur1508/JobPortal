using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
     
    {
        private readonly IJobService _jobService;


        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        } 
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetAllAsync()
        {
            var jobs = await _jobService.GetAllAsync();
            return Ok(new { Message = "List of the jobs", Data = jobs });
        }
        //GetbyId
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<JobDto>> GetById(Guid id)
        {
            var job = await _jobService.GetByIdAsync(id);

            if (job == null)
                return NotFound("Job not found.");

            return Ok(job);
        } //Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(JobDto jobDto)
        {
            await _jobService.AddAsync(jobDto);
            return Ok(new { Message = "Created a job", data = jobDto });

        }
        //update
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateJob(Guid id, [FromBody] JobDto jobDto)
        {
            await _jobService.UpdateAsync(id, jobDto);


            return Ok("Job updated successfully.");
        }

        //delete

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
