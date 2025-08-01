using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSkillController : ControllerBase
    {
        private readonly IJobSkillSevice _jobSkillService;

        public JobSkillController(IJobSkillSevice jobSkillService)
        {
            _jobSkillService = jobSkillService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobSkillDto>>> GetAllAsync()
        {
            var jobskills = await _jobSkillService.GetAllAsync();
            return Ok(new { Message = "List of the JobSkill", Data = jobskills });
        }
        [HttpPost("Add")]
        public async Task<ActionResult<JobSkillDto>> AddAsync(JobSkillDto jobskillDto)
        {
            await _jobSkillService.AddAsync(jobskillDto);
            return Ok(new { Message = "JobCourse is  create", data = jobskillDto });
        }
    }
}
