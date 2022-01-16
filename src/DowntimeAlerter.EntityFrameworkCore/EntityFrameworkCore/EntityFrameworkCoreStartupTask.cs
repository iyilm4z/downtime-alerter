using DowntimeAlerter.Engine;
using DowntimeAlerter.EntityFrameworkCore.Seed;

namespace DowntimeAlerter.EntityFrameworkCore
{
    public class EntityFrameworkCoreStartupTask : IStartupTask
    {
        public void Execute()
        {
            var dbContext = EngineContext.Current.Resolve<IDbContext>() as AppDbContext;

            SeedHelper.Seed(dbContext);
        }

        public int Order => 4;
    }
}
