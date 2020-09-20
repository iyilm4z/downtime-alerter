using DowntimeAlerter.Application.Services;
using DowntimeAlerter.Domain.Repositories;

namespace DowntimeAlerter.Authorization.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IRepository<User> _userRepository;

        public UserAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
    }
}