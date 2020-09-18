using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Views.Shared.Components.FooterLeft
{
    public class FooterLeftViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}