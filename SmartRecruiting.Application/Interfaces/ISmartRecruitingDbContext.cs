using Microsoft.EntityFrameworkCore;
using SmartRecruiting.Domain.Entities;
using SmartRecruiting.Domain.Entities.RelationalEntities;
using System.Threading;
using System.Threading.Tasks;

namespace SmartRecruiting.Application.Interfaces {
    public interface ISmartRecruitingDbContext {
        DbSet<Candidate> Candidates { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellation);

    }
}