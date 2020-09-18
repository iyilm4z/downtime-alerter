using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Views.Shared.Components.FooterRight
{
    public class FooterRightViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}