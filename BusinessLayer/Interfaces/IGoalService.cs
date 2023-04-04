using BusinessLayer.DTO;
using BusinessLayer.DTO.GoalsGetting;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<GoalCreationDTO> CreateGoal(GoalCreationDTO goal);
        GoalsListDTO GetGoals();
    }
}
