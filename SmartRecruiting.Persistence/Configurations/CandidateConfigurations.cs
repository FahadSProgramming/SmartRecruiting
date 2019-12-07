using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRecruiting.Domain.Entities;
using SmartRecruiting.Domain.Entities.RelationalEntities;

namespace SmartRecruiting.Persistence.Configurations {
    public class CandidateConfigurations : IEntityTypeConfiguration<Candidate> {
        public void Configure(EntityTypeBuilder<Candidate> builder) {

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(150);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(p => p.Description)
                .HasMaxLength(4000)
                .HasColumnType("ntext");

            builder.Property(p => p.City)
                .HasMaxLength(50);

            builder.Property(p => p.Province)
                .HasMaxLength(50);

            builder.Property(p => p.PostalCode)
                .HasMaxLength(20);

            builder.Property(p => p.Country)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}