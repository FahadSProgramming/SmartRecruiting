using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRecruiting.Domain {
    public class BaseEntity {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}