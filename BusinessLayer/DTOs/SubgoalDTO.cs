namespace BusinessLayer.DTOs
{
    public class SubgoalDTO
    {
        public string Title { get; set; } = string.Empty;
        public int Progress { get; set; } = 0;
        public IList<GoalTaskDTO> Tasks { get; set; } = new List<GoalTaskDTO>();
    }
}
