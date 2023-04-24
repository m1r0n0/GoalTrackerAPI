using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<GoalForCreationDTO> CreateGoal(GoalForCreationDTO goal);
        Task<GoalsListForGettingDTO> GetGoals();
        Task<GoalsListForGettingDTO> GetGoalsForUser(string userId);
    }
}
