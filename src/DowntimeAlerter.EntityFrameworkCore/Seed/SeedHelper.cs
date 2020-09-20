using DowntimeAlerter.Seed.Authorization.Users;

namespace DowntimeAlerter.Seed
{
    public static class SeedHelper
    {
        public static void Seed(AppDbContext context)
        {
            new UserSeeder(context).Create();
        }
    }
}
