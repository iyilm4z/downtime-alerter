using DowntimeAlerter.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : AppApiControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Api works!";
        }
    }
}
