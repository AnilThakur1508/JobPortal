using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCourseController : ControllerBase
    {
        private readonly IJobCourseService _jobCourseService;

        public JobCourseController(IJobCourseService jobCourseService)
        {
            _jobCourseService = jobCourseService;
        }
       
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<JobCourseDto>>> GetAllAsync()
        {
            var jobcourses = await _jobCourseService.GetAllAsync();
            return Ok(new { Message = "List of the JobCourse", Data = jobcourses });
        }
        [HttpPost("Add")]
        public async Task<ActionResult<JobCourseDto>> AddAsync(JobCourseDto jobcourseDto)
        {
            await _jobCourseService.AddAsync(jobcourseDto);
            return Ok(new { Message = "JobCourse is  create", data = jobcourseDto });
        }
    }
}
