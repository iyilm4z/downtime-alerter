using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Views.Shared.Components.SidebarUserPanel
{
    public class SidebarUserPanelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}