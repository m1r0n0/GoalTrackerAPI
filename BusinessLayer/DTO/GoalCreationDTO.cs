using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.DTO
{
    public class GoalCreationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public int Progress { get; set; }
        public int Duration { get; set; }
        public string Theme { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        [NotMapped]
        public List<string>? MembersIds { get; set; } = new List<string>();
    }
}
