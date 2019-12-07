using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartRecruiting.Application.Interfaces;
using SmartRecruiting.Domain.Entities;
using SmartRecruiting.Domain.Entities.RelationalEntities;

namespace SmartRecruiting.Persistence {
    public class SmartRecruitingDbContext : DbContext, ISmartRecruitingDbContext {
        public SmartRecruitingDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
            AddDateTimeAuditing(ChangeTracker);
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfigurationsFromAssembly(typeof(SmartRecruitingDbContext).Assembly);
        }

        #region Method : AddDateTimeAuditing (private - void)
        // Add auditing
        private void AddDateTimeAuditing(Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker tracker) {
            // for created entities
            tracker.Entries().Where(e => e.State == EntityState.Added).ToList()
                .ForEach(entity => {
                    entity.Property("CreatedAt").CurrentValue = System.DateTime.UtcNow;
                    entity.Property("ModifiedAt").CurrentValue = System.DateTime.UtcNow;
                });

            // for modified entities
            tracker.Entries().Where(e => e.State == EntityState.Modified).ToList()
                .ForEach(entity => {
                    entity.Property("ModifiedAt").CurrentValue = System.DateTime.UtcNow;
                });

        }
        #endregion
    }
}
//    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="3.1.0" />