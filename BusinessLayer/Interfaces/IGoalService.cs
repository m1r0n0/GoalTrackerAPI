using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;
using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<GoalTask> AddTask(GoalTask task);
        Task<GoalForCreationDTO> CreateGoal(GoalForCreationDTO goal);
        Task<GoalForGettingDTO> EditGoal(GoalForCreationDTO goal);
        Task<GoalsListForGettingDTO> GetGoalsForUser(string userId);
    }
}
