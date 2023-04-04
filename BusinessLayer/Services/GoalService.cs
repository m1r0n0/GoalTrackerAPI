using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.DTO.GoalsGetting;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class GoalService : IGoalService
    {
        private readonly IConfiguration _configuration;
        private readonly DataAccessLayer.Data.GoalContext _context;
        private readonly IMapper _mapper;

        public GoalService(DataAccessLayer.Data.GoalContext context,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<GoalCreationDTO> CreateGoal(GoalCreationDTO goal)
        {
            _context.GoalList.Add(_mapper.Map<Goal>(goal));
            await _context.SaveChangesAsync();
            return goal;
        }

        public GoalsListDTO GetGoals()
        {
            GoalsListDTO goalsList = new();
            foreach (Goal goal in _context.GoalList)
            {
                GoalForGettingDTO goalToAdd = _mapper.Map<GoalForGettingDTO>(goal);
                var tasksForCurrentGoal = _context.GoalTasks.Where(task => task.GoalId == goal.Id).ToList();
                List<UserForGettingDTO> membersOfCurrentGoal = new()
                {
                    new UserForGettingDTO("ABBA"),
                    new UserForGettingDTO("Kate Bush")
                };

                goalToAdd.Members = membersOfCurrentGoal;
                foreach (var task in tasksForCurrentGoal)
                {
                    goalToAdd.Tasks.Add(_mapper.Map<GoalTaskForGetting>(task));
                }

                goalsList.Goals.Add(goalToAdd);
            }
            return goalsList;
        }
    }
}
