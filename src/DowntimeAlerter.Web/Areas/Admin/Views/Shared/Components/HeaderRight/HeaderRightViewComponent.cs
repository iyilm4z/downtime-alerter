using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Views.Shared.Components.HeaderRight
{
    public class HeaderRightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}