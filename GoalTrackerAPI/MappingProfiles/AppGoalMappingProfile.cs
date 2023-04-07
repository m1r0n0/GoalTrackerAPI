using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;
using DataAccessLayer.Models;

namespace GoalTrackerAPI.MappingProfiles
{
    public class AppGoalMappingProfile : Profile
    {
        public AppGoalMappingProfile()
        {
            CreateMap<Goal, GoalForCreationDTO>().ReverseMap();
            CreateMap<GoalTask, GoalTaskDTO>().ReverseMap();
            CreateMap<Member, MemberForCreationDTO>().ReverseMap();
            CreateMap<Goal, SubgoalDTO>().ReverseMap();
            CreateMap<Goal, GoalForGettingDTO>().ReverseMap();
            CreateMap<GoalTask, GoalTaskDTO>().ReverseMap();
        }
    }
}
