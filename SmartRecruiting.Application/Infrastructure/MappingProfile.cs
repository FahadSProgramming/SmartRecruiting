using AutoMapper;
using SmartRecruiting.Application.Users;
using SmartRecruiting.Domain.Entities;

namespace SmartRecruiting.Application.Infrastructure {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(
                    src => src.Username.ToLower()
                ));
        }
    }
}