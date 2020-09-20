using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Views.Shared.Components.SidebarMenu
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}