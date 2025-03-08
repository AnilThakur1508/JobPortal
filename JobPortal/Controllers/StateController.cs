using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

[Route("api/states")]
[ApiController]
public class StateController : ControllerBase
{
   

    
    
        private readonly IStateService _service;
        public StateController(IStateService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateDto>>> GetAllAsync()
        {
            var states = await _service.GetAllAsync();
            return Ok(states);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<StateDto>> GetStateById(Guid Id)
        {
        var state = await _service.GetByIdAsync(Id);

            if (state == null)
            {
                return NotFound(new { Message = "State not found" });
            }
            return Ok(state);
        }
    
}
  

