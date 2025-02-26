using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        
        
            private readonly IStateService _stateService;

            public StateController(IStateService stateService)
            {
                _stateService = stateService;
            }
            //GetAll
            [HttpGet("GetAll")]
            public async Task<ActionResult<IEnumerable<StateDto>>> GetAllAsync()
            {
                var state = await _stateService.GetAllAsync();
                return Ok(new { Message = "List of the state", Data = state });
            }

            //Add 
            [HttpPost("Add")]
            public async Task<IActionResult> AddAsync(StateDto stateDto)
            {
                await _stateService.AddAsync(stateDto);
                return Ok(new { Message = "Created a state", data = stateDto });
            }
        
    }
}
