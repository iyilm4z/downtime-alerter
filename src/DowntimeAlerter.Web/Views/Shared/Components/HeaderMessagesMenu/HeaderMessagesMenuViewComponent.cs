using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Views.Shared.Components.HeaderMessagesMenu
{
    public class HeaderMessagesMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}