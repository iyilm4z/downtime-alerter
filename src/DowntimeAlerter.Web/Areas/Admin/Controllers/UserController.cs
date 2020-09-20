using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Authorization.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DowntimeAlerter.Web.Areas.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public IActionResult Index() => RedirectToAction(nameof(List));

        public IActionResult List()
        {
            var dtos = _userAppService.GetAll();
            return View(dtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var dto = _userAppService.GetForEdit(id);
            return View(dto);
        }

        [HttpPost]
        public IActionResult CreateOdEdit(UserEditDto editDto)
        {
            if (editDto.Id <= 0)
            {
                editDto.Role = UserRole.User;
            }

            _userAppService.CreateOrEdit(editDto);
            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int id)
        {
            _userAppService.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
