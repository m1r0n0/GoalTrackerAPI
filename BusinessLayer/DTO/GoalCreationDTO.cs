using DataAccessLayer.Models;

namespace BusinessLayer.DTO
{
    public class GoalCreationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string DateOfBeginning { get; set; } = string.Empty;
        public string DateOfEnding { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public IList<Member>? MembersIds { get; set; } = new List<Member>();
        public IList<GoalTask>? Tasks { get; set; } = new List<GoalTask>();
    }
}
