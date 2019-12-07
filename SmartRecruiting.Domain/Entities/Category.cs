using System.Collections.Generic;
using SmartRecruiting.Domain.Entities.RelationalEntities;

namespace SmartRecruiting.Domain.Entities {
    public class Category : BaseEntityWithAuditing {
        public string Name { get; set; }
        public ICollection<SkillBookCategory> SkillCategories { get; set; }
    }
}