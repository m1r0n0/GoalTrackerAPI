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
                .ForMember(user);
        }
    }
}
