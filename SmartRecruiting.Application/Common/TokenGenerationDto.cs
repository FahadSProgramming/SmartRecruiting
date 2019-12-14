using System;

namespace SmartRecruiting.Application.Common {
    public class TokenGenerationDto {
        public Guid UserId { get; set; }
        public string NameIdentifier { get; set; }
    }

    public class TokenParameters {
        public string SecurityKey { get; set; }
        public int ExpiryInDays { get; set; }
    }
}