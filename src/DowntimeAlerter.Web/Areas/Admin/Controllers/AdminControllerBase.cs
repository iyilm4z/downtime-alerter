using DowntimeAlerter.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Controllers
{
    [Area(AppDefaults.AdminAreaName)]
    [Authorize]
    public abstract class AdminControllerBase : AppControllerBase
    {
    }
}