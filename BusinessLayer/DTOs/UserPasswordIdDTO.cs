namespace BusinessLayer.DTOs
{
    public class UserPasswordIdDTO
    {
        public string? NewPassword { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public UserPasswordIdDTO(string userId)
        {
            UserId = userId;
        }

        public UserPasswordIdDTO() { }
    }
}
