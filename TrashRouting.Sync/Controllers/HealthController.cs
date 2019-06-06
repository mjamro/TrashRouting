using Microsoft.AspNetCore.Mvc;

namespace TrashRouting.Sync.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new OkResult();
        }
    }
}