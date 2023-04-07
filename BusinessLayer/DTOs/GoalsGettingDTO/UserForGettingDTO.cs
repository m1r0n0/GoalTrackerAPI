namespace BusinessLayer.DTOs.GoalsGettingDTO
{
    public class UserForGettingDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; }

        public UserForGettingDTO(string name)
        {
            Name = name;
            UserId = name + "Id";
        }
    }
}
