using DowntimeAlerter.Logging;
using DowntimeAlerter.Logging.Dto;
using DowntimeAlerter.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DowntimeAlerter.Web.Controllers
{
    [Route("api/[controller]")]
    public class LogController : AppApiControllerBase, ILogAppService
    {
        private readonly ILogAppService _logAppService;

        public LogController(ILogAppService logAppService)
        {
            _logAppService = logAppService;
        }

        [HttpGet]
        public List<LogListDto> GetAll()
        {
            var logs = _logAppService.GetAll();

            return logs;
        }

        [HttpGet("{id:int}")]
        public LogListDto GetForView(int id)
        {
            var log = _logAppService.GetForView(id);

            return log;
        }

        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            _logAppService.Delete(id);
        }

        [HttpDelete]
        public void DeleteAll()
        {
            _logAppService.DeleteAll();
        }
    }
}