using BusinessLayer.DTO.GoalCreationDTO;
using BusinessLayer.DTO.GoalsGetting;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<GoalForCreationDTO> CreateGoal(GoalForCreationDTO goal);
        Task<GoalsListDTO> GetGoals();
    }
}
