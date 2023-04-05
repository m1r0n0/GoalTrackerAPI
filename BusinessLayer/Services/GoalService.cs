using AutoMapper;
using BusinessLayer.DTO.GoalCreationDTO;
using BusinessLayer.DTO.GoalsGetting;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<GoalForCreationDTO> CreateGoal(GoalForCreationDTO goal)
        {
            var mainGoal = _mapper.Map<Goal>(goal);
            _context.GoalList.Add(mainGoal);
            await _context.SaveChangesAsync();
            if (!goal.IsComplex) return goal;

            if (goal.SubGoals != null)
                foreach (var subGoal in goal.SubGoals)
                {
                    var mappedSubGoal = _mapper.Map<Goal>(subGoal);
                    mappedSubGoal.MainGoalId = mainGoal.Id;
                    _context.GoalList.Add(mappedSubGoal);
                }
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<GoalsListDTO> GetGoals()
        {
            GoalsListDTO goalsList = new();
            foreach (var goal in _context.GoalList)
            {
                var goalToAdd = _mapper.Map<GoalForGettingDTO>(goal);
                var tasksForCurrentGoal = await _context.GoalTasks.Where(task => task.GoalId == goal.Id).ToListAsync();

                goalToAdd.Creator = new UserForGettingDTO(goal.CreatorId);

                var members = await _context.MembersIds.Where(member => member.GoalId == goal.Id).ToListAsync();
                var membersOfCurrentGoal = members.Select(member => new UserForGettingDTO(member.MemberId)).ToList();


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
