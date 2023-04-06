using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGetting;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<GoalForCreationDTO> CreateGoal(GoalForCreationDTO goal);
        Task<GoalsListForGettingDTO> GetGoals();
    }
}
