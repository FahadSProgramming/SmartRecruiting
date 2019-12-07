using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRecruiting.Domain.Entities {
    public class BaseEntityWithAuditing {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public User ModifiedBy { get; set; }
        public Guid ModifiedById { get; set; }
    }
}