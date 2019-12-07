using AutoMapper;
using SmartRecruiting.Application.Users;
using SmartRecruiting.Domain.Entities;

namespace SmartRecruiting.Application.Infrastructure {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<CreateUserCommand, User>();
        }
    }
}