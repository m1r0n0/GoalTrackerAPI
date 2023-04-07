﻿namespace DataAccessLayer.Models
{
    public class GoalTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int GoalId { get; set; }
    }
}
