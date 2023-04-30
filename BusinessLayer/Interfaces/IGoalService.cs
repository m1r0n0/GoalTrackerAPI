using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;
using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<GoalForCreationDTO> CreateGoal(GoalForCreationDTO goal);
        Task<Goal> EditGoal(GoalForCreationDTO goal);
        Task<GoalsListForGettingDTO> GetGoalsForUser(string userId);
    }
}
