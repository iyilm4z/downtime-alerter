using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Views.Shared.Components.SidebarBrand
{
    public class SidebarBrandViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}