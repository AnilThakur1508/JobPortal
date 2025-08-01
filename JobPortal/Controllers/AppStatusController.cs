using DataAccessLayer.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [Route("api/AppStatus")]
    [ApiController]
    public class AppStatusController : ControllerBase
    {
        [HttpGet("statuses")]
        public IActionResult GetStatuses()
        {
            var statuses = Enum.GetValues(typeof(AppStatus))
                .Cast<AppStatus>()
                .Select(s => new { Id = (int)s, StatusName = s.ToString() })
                .ToList();
            return new ObjectResult(statuses) { StatusCode = 200 };
        }
    }
}
