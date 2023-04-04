namespace DataAccessLayer.Models
{
    public class GoalTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int GoalId { get; set; }
    }
}
