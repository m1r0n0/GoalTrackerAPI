namespace DataAccessLayer.Models
{
    public class Subgoal
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Progress { get; set; } = 0;
        public int MainGoalId { get; set; }
        public string CreatorId { get; set; } = string.Empty;
        public IList<GoalTask> Tasks { get; set; } = new List<GoalTask>();
    }
}
