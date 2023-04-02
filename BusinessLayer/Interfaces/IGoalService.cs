using BusinessLayer.DTO;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<GoalCreationDTO> CreateGoal(GoalCreationDTO goal);
    }
}
