using System;
using System.Linq;
using System.Reflection;
using DowntimeAlerter.Domain.Entities;
using DowntimeAlerter.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.EntityFrameworkCore
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => (type.BaseType?.IsGenericType ?? false) 
                               && type.BaseType.GetGenericTypeDefinition() == typeof(AppEntityTypeConfiguration<>));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration?.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }

        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : Entity => base.Set<TEntity>();
    }
}