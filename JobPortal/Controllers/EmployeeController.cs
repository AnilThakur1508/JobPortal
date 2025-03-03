using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("api/Employe")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
        }
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllAsync()
        {
            var employee = await _employeeService.GetAllAsync();
            return Ok(new {Message =  "List of the employe", Data = employee});
        }
        //GetbyId
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
        {
            var employee = await _employeeService.GetByIdAsync(id)
;
            if (employee == null)
                return NotFound("Employee not found.");

            return Ok(employee);
        }

        //Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(EmployeeDto employeeDto)
        {
            await _employeeService.AddAsync(employeeDto);
            return Ok(new { Message = "Created a employee", data = employeeDto });

        }
        //update
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.UpdateAsync(id, employeeDto);


            return Ok("Employee updated successfully.");
        }
        //delete

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var success = await _employeeService.DeleteAsync(id)
;
            if (!success)
                return NotFound("Employee not found.");

            return Ok("Employe deleted successfully.");
        }

        
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest(new { message = "No file uploaded." });
        //    }

        //    try
        //    {
        //        string fileUrl = await _employeeService.UploadFileAsync(file);
        //        return Ok(new { filePath = fileUrl });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "File upload failed.", error = ex.Message });
        //    }
        //}
        




        //[HttpPost("update-employee")]
        //public async Task<IActionResult> SaveEmployee([FromForm] EmployeeDto employee)
        //{
        //    if (employee == null)
        //    {
        //        return BadRequest("Invalid employee profile data.");
        //    }

        //    var result = await _employeeService.AddAsync(employee);

        //    if (result == null)
        //        return StatusCode(500, "Failed to upload employee profile.");

        //    return Ok(new { message = "employee profile uploaded successfully!", employer = result });
        //}
        

    }


}




       
        
       





    


