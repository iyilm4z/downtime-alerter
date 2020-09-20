using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        public IActionResult Index() => View();
    }
}
