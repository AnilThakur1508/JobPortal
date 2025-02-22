using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("api/Qualification")]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationService _service;
        public  QualificationController(IQualificationService qualificationService)
        {
            _service = qualificationService;
        }
        //Get
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<QualificationDto>>> GetAllAsync()
        {
           var qualification = await _service.GetAllAsync();
            return Ok(new { Meassage = "List of the qualification", Data =qualification });
        }
        //Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(QualificationDto qualificationDto)
        {
           await _service.AddAsync(qualificationDto);
            return Ok(new { Meassage = "Added a qualification", Data = qualificationDto });
        }
    }
    
}
