using DowntimeAlerter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

        int SaveChanges();
    }
}