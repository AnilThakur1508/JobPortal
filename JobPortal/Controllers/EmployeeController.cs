using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Claims;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly object _logger;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id)
;
            if (employee == null)
                return NotFound("Employee not found.");

            return Ok(employee);
        }

        [HttpGet("GetByUserId/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetByUserId(string id)
        {
            var employee = await _employeeService.GetByUserIdAsync(Guid.Parse(id));

            if (employee == null)
                return NotFound("Employee not found.");

            return Ok(employee);
        }
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate(EmployeeDto employeeDto)
        {
            await _employeeService.UpsertAsync(employeeDto);
            return Ok(new { Message = "Created a employee", data = employeeDto });

        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var success = await _employeeService.DeleteAsync(id)
;
            if (!success)
                return NotFound("Employee not found.");

            return Ok("Employee deleted successfully.");
        }



      


    }

}





