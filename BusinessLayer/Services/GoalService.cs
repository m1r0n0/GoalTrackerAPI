using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;

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

            if (goal.SubGoals is not null)
                foreach (var subGoal in goal.SubGoals)
                {
                    var mappedSubGoal = _mapper.Map<Goal>(subGoal);
                    mappedSubGoal.MainGoalId = mainGoal.Id;
                    _context.GoalList.Add(mappedSubGoal);
                }
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<Goal> EditGoal(GoalForCreationDTO goal)
        {
            Goal goalToEdit = await _context.GoalList.Where(g => g.Id == goal.Id).FirstAsync();
            var mainGoal = _mapper.Map<GoalForEditDTO>(goal);
            goalToEdit = _mapper.Map<Goal>(mainGoal);
            await _context.SaveChangesAsync();
            return goalToEdit;
        }

        public async Task<GoalsListForGettingDTO> GetGoalsForUser(string userId)
        {
            int CalculateGoalProgress(Goal goal)
            {
                int amountOfCompleted = 0;
                foreach (GoalTask task in goal.Tasks)
                {
                    if (task.IsCompleted) amountOfCompleted++;
                }

                int result = Convert.ToInt32(Math.Floor(Convert.ToDecimal(amountOfCompleted * 100 / goal.Tasks.Count)));

                return result;
            }

            async Task<GoalForGettingDTO> AddSubgoalsToCurrentGoal(GoalForGettingDTO goal, List<Goal>? subgoals)
            {
                if (subgoals is null) return goal;
                foreach (Goal subgoal in subgoals)
                {
                    var subgoalToGet = _mapper.Map<SubgoalDTO>(subgoal);
                    subgoalToGet.Tasks = _mapper.Map<List<GoalTaskDTO>>(await _context.GoalTasks
                        .Where(task => task.GoalId == subgoal.Id)
                        .ToListAsync());
                    subgoalToGet.Progress = CalculateGoalProgress(subgoal);
                    goal.Subgoals.Add(subgoalToGet);
                }

                return goal;
            }

            GoalsListForGettingDTO goalsList = new();
            var goalsForCurrentUser = await _context.GoalList.Where(item => item.CreatorId == userId).ToListAsync();
            foreach (Goal goal in goalsForCurrentUser)
            {
                bool isSubgoal = goal.MainGoalId != null;
                if (!isSubgoal)
                {
                    var subgoalsOfCurrentGoal =
                        await _context.GoalList.Where(item => item.MainGoalId == goal.Id)?.ToListAsync()!;
                    bool isComplexGoal = subgoalsOfCurrentGoal.Count != 0;
                    var goalToGet = _mapper.Map<GoalForGettingDTO>(goal);

                    goalToGet.Creator = _mapper.Map<UserForGettingDTO?>(await _context.UserList
                        .Where(user => user.Id == goal.CreatorId).FirstOrDefaultAsync());
                    var members = await _context.MembersIds.Where(member => member.GoalId == goal.Id).ToListAsync();
                    goalToGet.Members = members.Select(member =>
                        _mapper.Map<UserForGettingDTO>(
                            _context.UserList.FirstOrDefault(user => user.Id == member.MemberId))).ToList();
                    goalToGet.isComplex = isComplexGoal;
                    if (isComplexGoal)
                    {
                        goalToGet = await AddSubgoalsToCurrentGoal(goalToGet, subgoalsOfCurrentGoal);
                        goalToGet.Progress = goalToGet.Subgoals.Sum(subgoal => subgoal.Progress) /
                                             goalToGet.Subgoals.Count;
                    }
                    else
                    {
                        var tasksForCurrentGoal =
                            await _context.GoalTasks.Where(task => task.GoalId == goal.Id).ToListAsync();
                        foreach (GoalTask task in tasksForCurrentGoal)
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

        public async Task<GoalTask> AddTask (GoalTask task)
        {
            _context.GoalTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<SubgoalDTO> AddSubGoal(SubgoalDTO subgoal)
        {
            _context
        }
    }
}
