using AutoMapper;
using BusinessLayer.DTO;
using DataAccessLayer.Models;

namespace ShortenUrlWebApi.MappingProfiles
{
    public class MappingProfileGoalToGoalCreationDTO : Profile
    {
        public MappingProfileGoalToGoalCreationDTO()
        {
            CreateMap<Goal, GoalCreationDTO>().ReverseMap();
        }
    }
}
