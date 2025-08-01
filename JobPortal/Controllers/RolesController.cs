using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles .ToList();


            if (roles.Count == 0)
                return NotFound("No roles found.");

            return Ok(roles);
        }
    }
}
