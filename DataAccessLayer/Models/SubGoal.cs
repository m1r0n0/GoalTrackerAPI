namespace DataAccessLayer.Models
{
    public class SubGoal
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Progress { get; set; } = 0;
        public IList<GoalTask> Tasks { get; set; } = new List<GoalTask>();
    }
}
