namespace BusinessLayer.DTOs.GoalsGettingDTO
{
    public class GoalsListForGettingDTO
    {
        public IList<GoalForGettingDTO> Goals { get; set; } = new List<GoalForGettingDTO>();
    }
}
