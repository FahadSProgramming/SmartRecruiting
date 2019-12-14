using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRecruiting.Application.Common;
using SmartRecruiting.Application.Exceptions;
using SmartRecruiting.Application.Interfaces;

namespace SmartRecruiting.Application.Users {
    public class LoginUserQuery : IRequest<AuthenticationResponse> {
        public string Username { get; set; }
        public string Password { get; set; }

        #region RequestHandler
        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthenticationResponse> {
            private readonly ISmartRecruitingDbContext _context;
            private readonly IJwtTokenGenerationService _jwtService;
            private readonly IPasswordGenerationService _passwordService;
            public LoginUserQueryHandler(ISmartRecruitingDbContext context, IJwtTokenGenerationService jwtService, IPasswordGenerationService passwordService) {
                _context = context;
                _jwtService = jwtService;
                _passwordService = passwordService;
            }
            public async Task<AuthenticationResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken) {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username.ToLower());

                if (user == null) {
                    throw new AuthException();
                }

                // compare password logic.
                if (!_passwordService.ComparePassword(request.Password, user.PasswordHash, user.PasswordSalt)) {
                    throw new AuthException();
                }
                // add jwt token service.
                var tokenResponse = _jwtService.GenerateToken(new TokenGenerationDto {
                    UserId = user.Id,
                    NameIdentifier = user.Username
                });

                // save token to db
                user.AuthToken = tokenResponse.Token;

                await _context.SaveChangesAsync(cancellationToken);
                return tokenResponse;
            }
        }
        #endregion

        #region RequestValidator
        public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery> {
            public LoginUserQueryValidator() {
                RuleFor(x => x.Username).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
            }
        }
        #endregion
    }
}