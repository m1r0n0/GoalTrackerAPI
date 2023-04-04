namespace BusinessLayer.DTO.GoalsGetting
{
    public class UserForGettingDTO
    {
        public string Name { get; set; } = "John";

        public UserForGettingDTO(string name)
        {
            Name = name;
        }
    }
}
