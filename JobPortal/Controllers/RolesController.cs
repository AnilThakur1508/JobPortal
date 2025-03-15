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

        // Get only "Employee" and "Employer" roles
        [HttpGet("all")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles
                .Where(r => r.Name == "Employee" || r.Name == "Employer") // Filter roles
                .Select(r => new { r.Id, r.Name })
                .ToList();

            return Ok(roles);
        }
    }
}
