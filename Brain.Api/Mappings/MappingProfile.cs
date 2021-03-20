using AutoMapper;
using Brain.Api.Models.Account;
using Brain.Api.Resources;

namespace Brain.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserSignupResource, User>()
                .ForMember(
                    u => u.UserName, 
                    opt => opt.MapFrom(ur => ur.Email
                    ))
                .ForMember(
                u => u.FirstName, 
                opt => opt.MapFrom(ur => ur.FirstName
                ))
                .ForMember(
                    u => u.LastName, 
                    opt => opt.MapFrom(ur => ur.LastName
            ));
        }
    }
}