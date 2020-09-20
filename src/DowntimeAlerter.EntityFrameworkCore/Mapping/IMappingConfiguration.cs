using Microsoft.EntityFrameworkCore;

namespace DowntimeAlerter.Mapping
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}