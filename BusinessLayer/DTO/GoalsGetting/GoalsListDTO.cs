namespace BusinessLayer.DTO.GoalsGetting
{
    public class GoalsListDTO
    {
        public IList<GoalForGettingDTO> Goals { get; set; } = new List<GoalForGettingDTO>();
    }
}
