namespace BusinessLayer.DTO.GoalCreationDTO
{
    public class GoalForCreationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsComplex { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string DateOfBeginning { get; set; } = string.Empty;
        public string DateOfEnding { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public IList<MemberForCreationDTO>? MembersIds { get; set; } = new List<MemberForCreationDTO>();
        public IList<SubGoalForCreationDTO>? SubGoals { get; set; } = new List<SubGoalForCreationDTO>();
        public IList<GoalTaskForCreationDTO>? Tasks { get; set; } = new List<GoalTaskForCreationDTO>();
    }
}
