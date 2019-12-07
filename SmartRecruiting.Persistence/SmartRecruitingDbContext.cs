using Microsoft.EntityFrameworkCore;
using SmartRecruiting.Application.Interfaces;
using SmartRecruiting.Domain.Entities;
using SmartRecruiting.Domain.Entities.RelationalEntities;

namespace SmartRecruiting.Persistence {
    public class SmartRecruitingDbContext : DbContext, ISmartRecruitmentDbContext {
        public SmartRecruitingDbContext(DbContextOptions options) : base(options) { }
    }
}
//    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="3.1.0" />