using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {



        private readonly IResumeService _resumeService;
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;

        public ResumeController(IResumeService resumeService, IWebHostEnvironment environment, AppDbContext context)
        {
            _resumeService = resumeService;
            _environment = environment;
            _context = context;
        }
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ResumeDto>>> GetAllAsync()
        {
            var resumes = await _resumeService.GetAllAsync();
            return Ok(new { Message = "List of the resumes", Data = resumes });
        }
        //GetbyId
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ResumeDto>> GetById(Guid id)
        {
            var resume = await _resumeService.GetByIdAsync(id);

            if (resume == null)
                return NotFound("Resume not found.");

            return Ok(resume);
        } //Add
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(ResumeDto resumeDto)
        {
            await _resumeService.AddAsync(resumeDto);
            return Ok(new { Message = "Created a resume", data = resumeDto });

        }
        //update
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Updateresume(Guid id, [FromBody] ResumeDto resumeDto)
        {
            await _resumeService.UpdateAsync(id, resumeDto);


            return Ok("Resume updated successfully.");
        }

        //delete

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteResume(Guid id)
        {
            var success = await _resumeService.DeleteAsync(id);

            if (!success)
                return NotFound("Resume not found.");

            return Ok("Resume deleted successfully.");
        }

        //    [HttpPost("{Id}/upload")]
        //    public async Task<ActionResult<bool>> UploadImageAsync(Guid Id, IFormFile image)
        //    {
        //        if (image == null)
        //        {
        //            return BadRequest("Invalid image file.");
        //        }

        //        var uploadPath = Path.Combine(_environment.WebRootPath, "uploads".ToString());
        //        if (!Directory.Exists(uploadPath))
        //        {
        //            Directory.CreateDirectory(uploadPath);
        //        }

        //        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
        //        var filePath = Path.Combine(uploadPath, fileName);

        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await image.CopyToAsync(stream);
        //        }

        //        var relativePath = Path.Combine("uploads".ToString(), fileName);

        //        var resumeImage = new Resume
        //        {

        //            ResumeFile = relativePath
        //        };

        //        _context.Resumes.Add(resumeImage);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "Image uploaded successfully", imagePath = relativePath });
        //    }
    }
}

   
