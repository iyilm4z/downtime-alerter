using DowntimeAlerter.Monitoring;
using DowntimeAlerter.Monitoring.Dto;
using DowntimeAlerter.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DowntimeAlerter.Web.Controllers
{
    [Route("api/[controller]")]
    public class TargetAppController : AppApiControllerBase, ITargetApplicationAppService
    {
        private readonly ITargetApplicationAppService _targetApplicationAppService;

        public TargetAppController(ITargetApplicationAppService targetApplicationAppService)
        {
            _targetApplicationAppService = targetApplicationAppService;
        }

        [HttpGet]
        public List<TargetApplicationListDto> GetAll()
        {
            var targetApps = _targetApplicationAppService.GetAll();

            return targetApps;
        }

        [HttpGet("{id:int}")]
        public TargetApplicationEditDto GetForEdit(int id)
        {
            var targetApp = _targetApplicationAppService.GetForEdit(id);

            return targetApp;
        }

        [HttpPost]
        public void CreateOrEdit(TargetApplicationEditDto editDto)
        {
            _targetApplicationAppService.CreateOrEdit(editDto);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _targetApplicationAppService.Delete(id);
        }
    }
}
