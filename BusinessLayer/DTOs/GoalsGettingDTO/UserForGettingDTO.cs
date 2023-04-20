namespace BusinessLayer.DTOs.GoalsGettingDTO
{
    public class UserForGettingDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;

        /*public UserForGettingDTO(string name)
        {
            Name = name;
            UserId = Id;
        }*/
    }
}
