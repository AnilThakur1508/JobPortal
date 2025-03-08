using DTO;
using Microsoft.AspNetCore.Mvc;

[Route("api/address")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;

    }
    [HttpPost("Add")]
    public async Task<ActionResult<AddressDto>> AddAsync(AddressDto addressDto)
    {
        await _addressService.AddAsync(addressDto);
        return Ok(new { Message = "Address is  create", data = addressDto });
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAllAsync()
    {
        var addresses = await _addressService.GetAllAsync();
        return Ok(new { Message = "List of the address ", Data = addresses });
    }
}
