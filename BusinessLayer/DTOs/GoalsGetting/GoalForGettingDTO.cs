namespace BusinessLayer.DTOs.GoalsGetting
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
        public UserForGettingDTO? Creator { get; set; }
        public IList<UserForGettingDTO> Members { get; set; } = new List<UserForGettingDTO>();
        public IList<GoalTaskDTO> Tasks { get; set; } = new List<GoalTaskDTO>();
        public IList<SubgoalDTO> Subgoals { get; set; } = new List<SubgoalDTO>();

        public GoalForGettingDTO(IList<UserForGettingDTO> members, IList<GoalTaskDTO> tasks)
        {
            Members = members;
            Tasks = tasks;
        }

        public GoalForGettingDTO() { }
    }
}
