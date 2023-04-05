using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.DTO.GoalsGetting;
using DataAccessLayer.Models;

namespace ShortenUrlWebApi.MappingProfiles
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Goal, GoalCreationDTO>().ReverseMap();
            CreateMap<Goal, GoalForGettingDTO>().ReverseMap();
            CreateMap<GoalTask, GoalTaskForGetting>().ReverseMap();

        }
    }
}
