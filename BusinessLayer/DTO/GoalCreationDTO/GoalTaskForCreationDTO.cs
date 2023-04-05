namespace BusinessLayer.DTO.GoalCreationDTO
{
    public class GoalTaskForCreationDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
