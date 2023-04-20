namespace BusinessLayer.DTOs
{
    public class UserEmailIdDTO
    {
        public string? NewEmail { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public UserEmailIdDTO(string userId)
        {
            UserId = userId;
        }

        public UserEmailIdDTO() { }
    }
}
