namespace BusinessLayer.DTO.GoalCreationDTO
{
    public class SubGoalForCreationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Progress { get; set; } = 0;
        public IList<GoalTaskForCreationDTO> Tasks { get; set; } = new List<GoalTaskForCreationDTO>();
    }
}
