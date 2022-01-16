using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Views.Shared.Components.HeaderNotificationsMenu
{
    public class HeaderNotificationsMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}