using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class User : IdentityUser
    {
        //No achieves and rememberMe
        public string Name { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public string DateOfSingUp { get; set; } = string.Empty;


    }
}
