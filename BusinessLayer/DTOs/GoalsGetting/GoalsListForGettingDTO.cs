namespace BusinessLayer.DTOs.GoalsGetting
{
    public class GoalsListForGettingDTO
    {
        public IList<GoalForGettingDTO> Goals { get; set; } = new List<GoalForGettingDTO>();
    }
}
