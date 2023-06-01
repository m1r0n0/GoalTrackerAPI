using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;
using BusinessLayer.DTOs.UserDTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class GoalService : IGoalService
    {
        private readonly DataAccessLayer.Data.ApplicationContext _context;
        private readonly IMapper _mapper;

        public GoalService(DataAccessLayer.Data.ApplicationContext context,
            IMapper mapper
        )
        {
            _context = context;
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
                    mappedSubGoal.CreatorId = mainGoal.CreatorId;
                    _context.GoalList.Add(mappedSubGoal);
                }
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<GoalForGettingDTO> EditGoal(GoalForCreationDTO goal)
        {
            bool isComplexGoal = goal.SubGoals is not null;
            GoalForGettingDTO editedGoal = await MapFieldsToGoalDBEntity(goal);
            if (isComplexGoal)
            {
                if (goal.SubGoals != null) editedGoal.Subgoals = await MapFieldsToSubgoalDBEntities(goal.SubGoals);
            }
            else
            {
                if (goal.Tasks != null) editedGoal.Tasks = await MapFieldsToTaskDBEntities(goal.Tasks);
            }
            return editedGoal;

            async Task<GoalForGettingDTO> MapFieldsToGoalDBEntity(GoalForCreationDTO goalToMapFrom)
            {
                Goal goalToEdit = await _context.GoalList.Where(g => g.Id == goal.Id).FirstAsync();
                goalToEdit.Title = goalToMapFrom.Title;
                goalToEdit.Category = goalToMapFrom.Category;
                goalToEdit.CreatorId = goalToMapFrom.CreatorId;
                goalToEdit.DateOfBeginning = goalToMapFrom.DateOfBeginning;
                goalToEdit.DateOfEnding = goalToMapFrom.DateOfEnding;
                goalToEdit.Description = goalToMapFrom.Description;
                goalToEdit.Priority = goalToMapFrom.Priority;
                goalToEdit.Status = goalToMapFrom.Status;
                goalToEdit.Theme = goalToMapFrom.Theme;
                await _context.SaveChangesAsync();
                goalToEdit = _mapper.Map<Goal>(goalToMapFrom);
                return _mapper.Map<GoalForGettingDTO>(goalToEdit);
            }

            async Task<List<Subgoal>> MapFieldsToSubgoalDBEntities(IList<Subgoal> subgoalsListToMapFrom)
            {
                List<Subgoal> subgoals = new List<Subgoal>();
                foreach (var subgoalToMapFrom in subgoalsListToMapFrom)
                {
                    var subgoalToEdit = await _context.GoalList.Where(g => g.MainGoalId == subgoalToMapFrom.MainGoalId).FirstAsync();
                    subgoalToEdit.Title = subgoalToMapFrom.Title;
                    subgoalToEdit.MainGoalId = subgoalToMapFrom.MainGoalId;
                    subgoalToEdit.Tasks =
                        await MapFieldsToTaskDBEntities(subgoalToMapFrom.Tasks); 
                    subgoals.Add(_mapper.Map<Subgoal>(subgoalToEdit));
                }
                await _context.SaveChangesAsync();
                return subgoals;
            }

            async Task<List<GoalTask>> MapFieldsToTaskDBEntities(IList<GoalTask> tasksListToMapFrom)
            {
                List<GoalTask> tasks = new List<GoalTask>();
                foreach (GoalTask taskToMapFrom in tasksListToMapFrom)
                {
                    GoalTask taskToEdit = await _context.GoalTasks.Where(t => t.GoalId == taskToMapFrom.GoalId).FirstAsync();
                    taskToEdit.Title = taskToMapFrom.Title;
                    taskToEdit.IsCompleted = taskToMapFrom.IsCompleted;
                    taskToEdit.Time = taskToMapFrom.Time;
                    tasks.Add(_mapper.Map<GoalTask>(taskToEdit));
                }
                await _context.SaveChangesAsync();
                return tasks;
            }
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
                    var subgoalToGet = _mapper.Map<Subgoal>(subgoal);
                    subgoalToGet.Tasks = await _context.GoalTasks
                        .Where(task => task.GoalId == subgoal.Id)
                        .ToListAsync();
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

                    goalToGet.Creator = _mapper.Map<UserToGetDTO?>(await _context.UserList
                        .Where(user => user.Id == goal.CreatorId).FirstOrDefaultAsync());
                    var members = await _context.MembersIds.Where(member => member.GoalId == goal.Id).ToListAsync();
                    goalToGet.Members = members.Select(member =>
                        _mapper.Map<UserToGetDTO>(
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
                            goalToGet.Tasks.Add(_mapper.Map<GoalTask>(task));
                        }

                        goalToGet.Progress = CalculateGoalProgress(goal);
                    }

                    goalsList.Goals.Add(goalToGet);
                }
            }

            return goalsList;
        }

        public async Task<GoalTask> AddTask(GoalTask task)
        {
            _context.GoalTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        //public async Task<SubgoalDTO
        //> AddSubGoal(SubgoalDTO subgoal)
        //{
        //  _context
        //}
    }
}
