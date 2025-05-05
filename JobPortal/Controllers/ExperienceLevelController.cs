using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceLevelController : ControllerBase
    {
        private readonly IExperienceLevelService _experienceLevelService;

        public ExperienceLevelController(IExperienceLevelService experienceLevelService)
        {
            _experienceLevelService = experienceLevelService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ExperienceLevelDto>>> GetAllAsync()
        {
            var experienceLevels = await _experienceLevelService.GetAllAsync();
            return Ok(experienceLevels);
        }
    }
}
