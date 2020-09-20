using DowntimeAlerter.Monitoring;
using DowntimeAlerter.Monitoring.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Controllers
{
    public class TargetAppController : AdminControllerBase
    {
        private readonly ITargetApplicationAppService _targetApplicationAppService;

        public TargetAppController(ITargetApplicationAppService targetApplicationAppService)
        {
            _targetApplicationAppService = targetApplicationAppService;
        }

        public IActionResult Index() => RedirectToAction(nameof(List));

        public IActionResult List()
        {
            var dtos = _targetApplicationAppService.GetAll();
            return View(dtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var dto = _targetApplicationAppService.GetForEdit(id);
            return View(dto);
        }

        [HttpPost]
        public IActionResult CreateOdEdit(TargetApplicationEditDto editDto)
        {
            _targetApplicationAppService.CreateOrEdit(editDto);
            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int id)
        {
            _targetApplicationAppService.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
