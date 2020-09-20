using System;
using System.Linq;
using DowntimeAlerter.Domain.Repositories;

namespace DowntimeAlerter.Authorization.Users
{
    public static class UserRepositoryExtensions
    {
        public static User GetUserByUsername(this IRepository<User> userRepository, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from x in userRepository.GetAll()
                        orderby x.Id
                        where x.UserName == username
                        select x;

            var user = query.FirstOrDefault();

            return user;
        }

        public static User GetUserByEmail(this IRepository<User> userRepository, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from x in userRepository.GetAll()
                        orderby x.Id
                        where x.Email == email
                        select x;

            var user = query.FirstOrDefault();

            return user;
        }

        public static User InsertGuestUser(this IRepository<User> userRepository)
        {
            var user = new User
            {
                UserGuid = Guid.NewGuid(),
                Role = UserRole.Guest
            };

            userRepository.Insert(user);

            return user;
        }

        public static User GetUserByGuid(this IRepository<User> userRepository, Guid userGuid)
        {
            if (userGuid == Guid.Empty)
                return null;

            var query = from x in userRepository.GetAll()
                        orderby x.Id
                        where x.UserGuid == userGuid
                        select x;

            var user = query.FirstOrDefault();

            return user;
        }
    }
}
