using DowntimeAlerter.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DowntimeAlerter.EntityFrameworkCore.Mapping.Authorization.Users
{
    public class UserMap : AppEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(user => user.Id);

            builder.Property(user => user.UserName).HasMaxLength(1000);
            builder.Property(user => user.Email).HasMaxLength(1000);

            builder.Ignore(user => user.Fullname);

            base.Configure(builder);
        }
    }
}
