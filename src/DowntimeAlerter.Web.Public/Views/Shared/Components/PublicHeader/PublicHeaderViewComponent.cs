using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Views.Shared.Components.PublicHeader
{
    public class PublicHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}