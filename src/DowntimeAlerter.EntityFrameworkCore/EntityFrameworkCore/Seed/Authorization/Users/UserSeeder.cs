using System.Linq;
using DowntimeAlerter.Authorization.Users;

namespace DowntimeAlerter.EntityFrameworkCore.Seed.Authorization.Users
{
    public class UserSeeder
    {
        private readonly AppDbContext _context;

        public UserSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var usersTable = _context.Set<User>();

            var adminUser = usersTable.FirstOrDefault(u => u.UserName == AppDefaults.AdminUserName);
            if (adminUser != null)
                return;

            var user = new User
            {
                UserName = AppDefaults.AdminUserName,
                Name = AppDefaults.AdminUserName,
                Surname = AppDefaults.AdminUserName,
                Email = $"{AppDefaults.AdminUserName}@da.com",
                Password = "123qwe",
                Role = UserRole.Admin
            };

            usersTable.Add(user);

            _context.SaveChanges();
        }
    }
}