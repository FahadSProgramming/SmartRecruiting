using System;

namespace SmartRecruiting.Domain.Entities.RelationalEntities {
    public class SkillBookCategory : BaseEntity {
        public SkillBook Skill { get; set; }
        public Guid SkillId { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}