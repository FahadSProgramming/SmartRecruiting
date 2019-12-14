using System;

namespace SmartRecruiting.Application.Common {
    public class TokenGenerationDto {
        public Guid UserId { get; set; }
        public string NameIdentifier { get; set; }
    }
}