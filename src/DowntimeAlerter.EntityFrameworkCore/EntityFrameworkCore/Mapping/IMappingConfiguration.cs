using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.EntityFrameworkCore.Mapping
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}