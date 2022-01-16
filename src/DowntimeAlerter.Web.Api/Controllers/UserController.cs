using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Authorization.Users.Dto;
using DowntimeAlerter.Web.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DowntimeAlerter.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : AppApiControllerBase, IUserAppService
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public List<UserListDto> GetAll([FromQuery] bool includeGuests = false)
        {
            var users = _userAppService.GetAll(includeGuests);

            return users;
        }

        [HttpGet("{id:int}")]
        public UserEditDto GetForEdit(int id)
        {
            var user = _userAppService.GetForEdit(id);

            return user;
        }


        [HttpPost]
        public void CreateOrEdit(UserEditDto editDto)
        {
            _userAppService.CreateOrEdit(editDto);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _userAppService.Delete(id);
        }
    }
}