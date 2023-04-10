using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.Models;

namespace GoalTrackerAPI.MappingProfiles
{
    public class AppUserMappingProfile : Profile
    {
        public AppUserMappingProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom((src => src.Email)))
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
        }
    }
}
