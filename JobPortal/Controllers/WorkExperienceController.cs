using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("api/WorkExperience")]
    public class WorkExperienceController : ControllerBase
    {
        private readonly IWorkExperienceService _experienceService;

        public WorkExperienceController(IWorkExperienceService experienceService)
        {
            _experienceService = experienceService;
        }
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<WorkExperience>>> GetAllAsync()
        {
            var experience = await _experienceService.GetAllAsync();
            return Ok(new { Meassage = "List of Work Experience", Data = experience });
        }

        //Add 
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(WorkExperienceDto workExperienceDto)
        {
            await _experienceService.AddAsync(workExperienceDto);
            return Ok(new { Meassage = "Added a Work Experience", Data = workExperienceDto });
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<WorkExperienceDto>> GetById(Guid id)
        {
            var experience = await _experienceService.GetByIdAsync(id);

            if (experience == null)
                return NotFound("Experience not found.");

            return Ok(experience);
        }
    }
}
