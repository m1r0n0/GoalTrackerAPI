using BusinessLayer.DTOs.UserDTOs;
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
        public UserToGetDTO? Creator { get; set; }
        public IList<UserToGetDTO> Members { get; set; } = new List<UserToGetDTO>();
        public IList<GoalTask> Tasks { get; set; } = new List<GoalTask>();
        public IList<Subgoal> Subgoals { get; set; } = new List<Subgoal>();
    }
}
