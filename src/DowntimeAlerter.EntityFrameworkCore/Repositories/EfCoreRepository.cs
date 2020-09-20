using System;
using DowntimeAlerter.Domain.Entities;
using DowntimeAlerter.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DowntimeAlerter.Repositories
{
    public class EfCoreRepository<TEntity> : RepositoryBase<TEntity> where TEntity : Entity
    {
        private readonly IDbContext _context;

        private DbSet<TEntity> _table;

        public EfCoreRepository(IDbContext context)
        {
            _context = context;
        }

        private string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public override IQueryable<TEntity> GetAll() => Table;

        public override void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Table.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public override void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Table.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public override void Delete(int id)
        {
            var entity = FirstOrDefault(id);
            if (entity != null)
                Delete(entity);
        }

        public override void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Table.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        private DbSet<TEntity> Table => _table ??= _context.Set<TEntity>();
    }
}
