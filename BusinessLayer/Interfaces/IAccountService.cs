using BusinessLayer.DTOs.UserDTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAccountService
    {
        UserEmailIdDTO GetUserIDFromUserEmail(string userEmail);
        UserEmailIdDTO GetUserEmailFromUserID(string userID);
        bool CheckGivenEmailForExistingInDB(string email);
        UserEmailIdDTO setNewUserEmail(string newUserEmail, string userID);
        Task<User> GetUserById(string Id);
    }
}
