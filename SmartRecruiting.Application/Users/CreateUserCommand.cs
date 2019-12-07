using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRecruiting.Application.Exceptions;
using SmartRecruiting.Application.Interfaces;
using SmartRecruiting.Domain.Entities;

namespace SmartRecruiting.Application.Users {
    public class CreateUserCommand : IRequest {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        #region CreateUserCommandHandler : IRequestHandler
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit> {
            private readonly ISmartRecruitingDbContext _context;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IMapper mapper, ISmartRecruitingDbContext context) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
                var userExists = await _context.Users.Where(x => x.Username == request.Username.ToLower()).AnyAsync();
                if (userExists) {
                    throw new DuplicateEntityException(nameof(User), request.Username);
                }
                var entity = _mapper.Map<User>(request);

                // TODO: populate the passwords
                entity.PasswordHash = null;
                entity.PasswordSalt = null;

                await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }

        }
        #endregion

        #region CreateUserCommandValidator
        public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
            public CreateUserCommandValidator() {
                RuleFor(x => x.FirstName)
                    .MaximumLength(150)
                    .NotNull()
                    .NotEmpty();

                RuleFor(x => x.LastName)
                    .MaximumLength(150)
                    .NotNull()
                    .NotEmpty();

                RuleFor(x => x.Username)
                    .MaximumLength(250)
                    .NotNull()
                    .NotEmpty();

                RuleFor(x => x.Password)
                    .MinimumLength(8)
                    .NotNull()
                    .NotEmpty();
            }
        }
        #endregion
    }
}