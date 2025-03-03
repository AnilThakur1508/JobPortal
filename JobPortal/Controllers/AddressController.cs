using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        //GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAsync()
        {
            var addresses = await _addressService.GetAllAsync();
            return Ok(new { Message = "List of the address ", Data = addresses });
        }

        //Add 
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(AddressDto addressDto)
        {
            await _addressService.AddAsync(addressDto);
            return Ok(new { Message = "Address is  create", data = addressDto });
        }

    }

}
