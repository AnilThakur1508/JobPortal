using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        
        
            private readonly ICountryService _countryService;

            public CountryController(ICountryService countryService)
            {
                _countryService = countryService;
            }
            //GetAll
            [HttpGet("GetAll")]
            public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllAsync()
            {
                var country = await _countryService.GetAllAsync();
                return Ok(new { Message = "List of the country", Data = country });
            }

            //Add 
            [HttpPost("Add")]
            public async Task<IActionResult> AddAsync(CountryDto countryDto)
            {
                await _countryService.AddAsync(countryDto);
                return Ok(new { Message = "Created a Country", data = countryDto });
            }
        
    }
}
