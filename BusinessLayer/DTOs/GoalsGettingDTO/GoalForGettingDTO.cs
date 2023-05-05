using DataAccessLayer.Models;

namespace BusinessLayer.DTOs.GoalsGettingDTO
{
    public class GoalForGettingDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool isComplex { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Progress { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public string DateOfBeginning { get; set; } = string.Empty;
        public string DateOfEnding { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public UserForGettingDTO? Creator { get; set; }
        public IList<UserForGettingDTO> Members { get; set; } = new List<UserForGettingDTO>();
        public IList<GoalTask> Tasks { get; set; } = new List<GoalTask>();
        public IList<Subgoal> Subgoals { get; set; } = new List<Subgoal>();
    }
}
