using DowntimeAlerter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.EntityFrameworkCore
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

        int SaveChanges();
    }
}