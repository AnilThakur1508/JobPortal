using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("api/Employe")]
    public class EmployeeController : ControllerBase
    {
        private readonly  IEmployeeService _employeService;

        public EmployeeController(IEmployeeService employeService)
        {
            _employeService = employeService;
        }
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllAsync()
        {
            var employe = await _employeService.GetAllAsync();
            return Ok(new {Message =  "List of the employe", Data = employe});
        }

        //Add 
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(EmployeeDto employeDto)
        {
          await _employeService.AddAsync(employeDto);
            return Ok(new {  Message = "Created a employe" ,data = employeDto});
        }
    }
}
