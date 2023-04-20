using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    [Index("MainGoalId")]
    public class Goal
    {
        public int Id { get; set; }
        public int? MainGoalId { get; set; } = null;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public int? Priority { get; set; } = null;
        public string? Status { get; set; } = null;
        public int Progress { get; set; } = 0;
        public string? Category { get; set; } = null;
        public string? DateOfBeginning { get; set; } = null;
        public string? DateOfEnding { get; set; } = null;
        public string? Theme { get; set; } = null;
        public string? CreatorId { get; set; } = null;
        public IList<Member> MembersIds { get; set; } = new List<Member>();
        public IList<GoalTask> Tasks { get; set; } = new List<GoalTask>();
    }
}
