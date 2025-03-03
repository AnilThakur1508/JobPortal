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

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(WorkExperienceDto experienceDto)
        {
            await _experienceService.AddAsync(experienceDto);
            return Ok(new { Message = "Created a Experience", data = experienceDto });
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<WorkExperienceDto>> GetById(Guid id)
        {
            var experience = await _experienceService.GetByIdAsync(id);

            if (experience == null)
                return NotFound("Experience not found.");

            return Ok(experience);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateWorkExperience(Guid id, [FromBody] WorkExperienceDto experienceDto)
        {
            await _experienceService.UpdateAsync(id, experienceDto);


            return Ok("Experience updated successfully.");
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteWorkExperience(Guid id)
        {
            var success = await _experienceService.DeleteAsync(id)
;
            if (!success)
                return NotFound("Experience not found.");

            return Ok("Experience deleted successfully.");
        }
        

    }
}
