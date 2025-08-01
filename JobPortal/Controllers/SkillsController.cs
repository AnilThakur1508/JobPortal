using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService _skillsService;

        public SkillsController(ISkillsService skillsService)
        {
            _skillsService = skillsService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SkillsDto>>> GetAllAsync()
        {
            var skills = await _skillsService.GetAllAsync();
            return Ok(new { Message = "List of the skills", Data = skills });
        }
        
        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<SkillsDto>>> GetByCategoryAsync(Guid categoryId)
        {
            var skills = await _skillsService.GetByCategoryAsync(categoryId);

            if (skills == null || !skills.Any())
            {
                return NotFound(new { Message = "No skills found for the selected category." });
            }

            return Ok(skills);
        }
    }   
      
}    

