using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Views.Shared.Components.PublicFooter
{
    public class PublicFooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}