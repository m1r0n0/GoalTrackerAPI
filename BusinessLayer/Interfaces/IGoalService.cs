using DataAccessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IGoalService
    {
        Task<Goal> CreateGoal(Goal goal);
    }
}
