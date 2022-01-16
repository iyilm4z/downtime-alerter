using DowntimeAlerter.EntityFrameworkCore.Seed.Authorization.Users;

namespace DowntimeAlerter.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void Seed(AppDbContext context)
        {
            new UserSeeder(context).Create();
        }
    }
}
