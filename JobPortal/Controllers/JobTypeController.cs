using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypeController : ControllerBase
    {
        private readonly IJobTypeService _jobTypeService;

        public JobTypeController(IJobTypeService jobTypeService)
        {
            _jobTypeService = jobTypeService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobTypeDto>>> GetAllAsync()
        {
            var jobTypes = await _jobTypeService.GetAllAsync();
            return Ok(jobTypes);
        }
    }
}
