using DowntimeAlerter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DowntimeAlerter.Mapping
{
    public class AppEntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity>
        where TEntity : Entity
    {
        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            PostConfigure(builder);
        }

        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }
}