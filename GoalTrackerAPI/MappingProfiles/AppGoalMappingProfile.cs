using AutoMapper;
using BusinessLayer.DTO.GoalCreationDTO;
using BusinessLayer.DTO.GoalsGetting;
using DataAccessLayer.Models;

namespace GoalTrackerAPI.MappingProfiles
{
    public class AppGoalMappingProfile : Profile
    {
        public AppGoalMappingProfile()
        {
            CreateMap<Goal, GoalForCreationDTO>().ReverseMap();
            CreateMap<GoalTask, GoalTaskForCreationDTO>().ReverseMap();
            CreateMap<Member, MemberForCreationDTO>().ReverseMap();
            CreateMap<Goal, SubGoalForCreationDTO>().ReverseMap();
            CreateMap<Goal, GoalForGettingDTO>().ReverseMap();
            CreateMap<GoalTask, GoalTaskForGetting>().ReverseMap();

        }
    }
}
