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
            [HttpPost("Add")]
            public async Task<ActionResult<CountryDto>> AddAsync(CountryDto countryDto)
            {
                await _countryService.AddAsync(countryDto);
                return Ok(new { Message = "Country is  create", data = countryDto });
            }

            [HttpGet("GetAll")]
            public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllAsync()
            {
                var countries = await _countryService.GetAllAsync();
                return Ok(new { Message = "List of the Country ", Data = countries });
            }

    }
}
