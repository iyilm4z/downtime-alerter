using System.Collections.Generic;
using DowntimeAlerter.Application.Services;
using DowntimeAlerter.Authorization.Users.Dto;

namespace DowntimeAlerter.Authorization.Users
{
    public interface IUserAppService : IApplicationService
    {
        List<UserListDto> GetAll(bool includeGuests = false);
        UserEditDto GetForEdit(int id);
        void CreateOrEdit(UserEditDto editDto);
        void Delete(int id);
    }
}