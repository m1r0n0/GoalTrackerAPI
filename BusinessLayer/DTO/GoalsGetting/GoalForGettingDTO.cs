namespace BusinessLayer.DTO.GoalsGetting
{
    public class GoalForGettingDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Priority { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Progress { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public string DateOfBeginning { get; set; } = string.Empty;
        public string DateOfEnding { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public IList<UserForGettingDTO> Members { get; set; } = new List<UserForGettingDTO>();
        public IList<GoalTaskForGetting> Tasks { get; set; } = new List<GoalTaskForGetting>();

        public GoalForGettingDTO(IList<UserForGettingDTO> members, IList<GoalTaskForGetting> tasks)
        {
            Members = members;
            Tasks = tasks;
        }

        public GoalForGettingDTO() { }
    }
}
