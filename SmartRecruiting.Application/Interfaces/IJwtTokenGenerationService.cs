using SmartRecruiting.Application.Common;

namespace SmartRecruiting.Application.Interfaces {
    public interface IJwtTokenGenerationService {
        AuthenticationResponse GenerateToken(TokenGenerationDto tokenGenerationDto);
    }
}