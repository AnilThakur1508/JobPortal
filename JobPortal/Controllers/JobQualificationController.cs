using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobQualificationController : ControllerBase
    {
        private readonly IJobQualificationService _jobQualificationService;

        public JobQualificationController(IJobQualificationService jobQualificationService)
        {
            _jobQualificationService = jobQualificationService;
        }
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobQualificationDto>>> GetAllAsync()
        {
            var jobQualifications = await _jobQualificationService.GetAllAsync();
            return Ok(new { Message = "List of the JobQualication", Data = jobQualifications });
        }
    }
}
