using DataAccessLayer.Entity;
using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Implementation;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobPortal.Controllers
{

    [Route("Api/Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
       public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        //Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _authenticateService.RegisterAsync(model);
            if (result == null)
                return BadRequest("User registration failed.");

            return Ok(new { message = result });
        }

        //Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _authenticateService.LoginAsync(model);
            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { token });
        }
        
        
    }
}
