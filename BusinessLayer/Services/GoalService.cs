using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class GoalService : IGoalService
    {
        private readonly IConfiguration _configuration;
        private readonly DataAccessLayer.Data.ApplicationContext _context;
        private readonly IMapper _mapper;

        public GoalService(DataAccessLayer.Data.ApplicationContext context,
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

        public async Task<GoalsListForGettingDTO> GetGoals()
        {
            int CalculateGoalProgress(Goal goal)
            {
                int amountOfCompleted = 0;
                foreach (GoalTask task in goal.Tasks)
                {
                    if (task.IsCompleted) amountOfCompleted++;
                }
                return (amountOfCompleted / goal.Tasks.Count) * 100;
            }

            GoalsListForGettingDTO goalsList = new();
            foreach (var goal in _context.GoalList)
            {
                bool isSubgoal = goal.MainGoalId != null;
                var subgoalsOfCurrentGoal = await _context.GoalList.Where(item => item.MainGoalId == goal.Id)?.ToListAsync()!;
                bool isComplexGoal = subgoalsOfCurrentGoal.Count != 0;
                if (!isSubgoal)
                {
                    var goalToGet = _mapper.Map<GoalForGettingDTO>(goal);
                    goalToGet.Creator = _mapper.Map<UserForGettingDTO?>(await _context.UserList
                        .Where(user => user.Id == goal.CreatorId).FirstOrDefaultAsync());
                    var members = await _context.MembersIds.Where(member => member.GoalId == goal.Id).ToListAsync();
                    goalToGet.Members = members.Select(member => _mapper.Map<UserForGettingDTO>(_context.UserList.FirstOrDefault(user => user.Id == member.MemberId))).ToList();
                    if (isComplexGoal)
                    {
                        foreach (Goal subgoal in subgoalsOfCurrentGoal)
                        {
                            var subgoalToGet = _mapper.Map<SubgoalDTO>(subgoal);
                            subgoalToGet.Tasks = _mapper.Map<List<GoalTaskDTO>>(await _context.GoalTasks.Where(task => task.GoalId == subgoal.Id)
                                .ToListAsync());
                            subgoalToGet.Progress = CalculateGoalProgress(subgoal);
                            goalToGet.Subgoals.Add(subgoalToGet);
                        }
                        goalToGet.Progress = goalToGet.Subgoals.Sum(subgoal => subgoal.Progress) / goalToGet.Subgoals.Count;
                    }
                    else
                    {
                        var tasksForCurrentGoal =
                            await _context.GoalTasks.Where(task => task.GoalId == goal.Id).ToListAsync();
                        foreach (var task in tasksForCurrentGoal)
                        {
                            goalToGet.Tasks.Add(_mapper.Map<GoalTaskDTO>(task));
                        }

                        goalToGet.Progress = CalculateGoalProgress(goal);
                    }
                    goalsList.Goals.Add(goalToGet);
                }
            }
            return goalsList;
        }
    }
}
