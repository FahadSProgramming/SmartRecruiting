using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruiting.Domain.Entities;
using SmartRecruiting.Domain.Entities.RelationalEntities;

namespace SmartRecruiting.Persistence.Configurations {
    public class UserConfigurations : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}