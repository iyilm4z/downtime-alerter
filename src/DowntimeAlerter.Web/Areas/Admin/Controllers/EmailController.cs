using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Controllers
{
    public class EmailController : AdminControllerBase
    {
        public IActionResult Index() => RedirectToAction(nameof(List));

        public IActionResult List()
        {
            return View();
        }
    }
}
