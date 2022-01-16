using DowntimeAlerter.Monitoring;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DowntimeAlerter.EntityFrameworkCore.Mapping.Monitoring
{
    public class TargetApplicationMap : AppEntityTypeConfiguration<TargetApplication>
    {
        public override void Configure(EntityTypeBuilder<TargetApplication> builder)
        {
            builder.ToTable(nameof(TargetApplication));
            builder.HasKey(application => application.Id);

            builder.Property(application => application.Name).HasMaxLength(1000);
            builder.Property(application => application.Url).HasMaxLength(1000);

            base.Configure(builder);
        }
    }
}
