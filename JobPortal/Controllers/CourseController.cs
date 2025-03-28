using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Implementation;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllAsync()
        {
            var course = await _courseService.GetAllAsync();
            return Ok(course);
        }
    }
}

