namespace BusinessLayer.DTOs
{
    public class CheckExistingEmailDTO
    {
        public string Email { get; set; } = string.Empty;
        public bool IsExist { get; set; }

        public CheckExistingEmailDTO(string email, bool isExist)
        {
            Email = email;
            IsExist = isExist;
        }
    }
}
