using DowntimeAlerter.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Controllers
{
    public class HomeController : AppControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}