using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    [Index("Id")]
    public class Goal
    {
        //Add goal progress when giving goals
        public int Id { get; set; }
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
        public IList<MembersIds> MembersIds { get; set; } = new List<MembersIds>();
        public IList<GoalTask> Tasks { get; set; } = new List<GoalTask>();
    }
}
