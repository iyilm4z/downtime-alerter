using System.Collections.Generic;
using System.Net.Http;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Authorization.Users.Dto;

namespace DowntimeAlerter.Web.ApiClient.Authorization
{
    public class UserApiClient : IUserAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<UserListDto> GetAll(bool includeGuests = false)
        {
            throw new System.NotImplementedException();
        }

        public UserEditDto GetForEdit(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CreateOrEdit(UserEditDto editDto)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}