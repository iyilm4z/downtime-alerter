using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DowntimeAlerter.Web.Models;

namespace DowntimeAlerter.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
