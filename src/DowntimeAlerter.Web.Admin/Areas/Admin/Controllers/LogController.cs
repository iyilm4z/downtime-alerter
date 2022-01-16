using DowntimeAlerter.Logging;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Controllers
{
    public class LogController : AdminControllerBase
    {
        private readonly ILogAppService _logAppService;

        public LogController(ILogAppService logAppService)
        {
            _logAppService = logAppService;
        }

        public IActionResult Index() => RedirectToAction(nameof(List));

        public IActionResult List()
        {
            var dtos = _logAppService.GetAll();
            return View(dtos);
        }

        public IActionResult View(int id)
        {
            var dto = _logAppService.GetForView(id);
            return View(dto);
        }

        public IActionResult Delete(int id)
        {
            _logAppService.Delete(id);
            return RedirectToAction(nameof(List));
        }

        public IActionResult DeleteAll()
        {
            _logAppService.DeleteAll();
            return RedirectToAction(nameof(List));
        }
    }
}