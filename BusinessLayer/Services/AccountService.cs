using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataAccessLayer.Data.ApplicationContext _context;
        public AccountService(DataAccessLayer.Data.ApplicationContext context)
        {
            _context = context;
        }
        public UserEmailIdDTO GetUserIDFromUserEmail(string userEmail)
        {
            UserEmailIdDTO userEmailIdDTO = new();
            userEmailIdDTO.NewEmail = userEmail;
            var tempUserEmailToIdDTO = _context.UserList?.Where(item => item.Email == userEmail)?.FirstOrDefault();
            if (tempUserEmailToIdDTO == null)
            {
                userEmailIdDTO.UserId = "";
            }
            else
            {
                userEmailIdDTO.UserId = tempUserEmailToIdDTO.Id;
            }
            return userEmailIdDTO;
        }

        public UserEmailIdDTO GetUserEmailFromUserID(string userId)
        {
            UserEmailIdDTO userEmailIdDTO = new();
            userEmailIdDTO.UserId = userId;
            var tempUserEmailToIdDTO = _context.UserList?.Where(item => item.Id == userId)?.FirstOrDefault();
            if (tempUserEmailToIdDTO == null)
            {
                userEmailIdDTO.NewEmail = "";
            }
            else
            {
                userEmailIdDTO.NewEmail = tempUserEmailToIdDTO.Email;
            }
            return userEmailIdDTO;
        }

        public bool CheckGivenEmailForExistingInDB(string email)
        {
            var tempModel = _context.UserList?.Where(item => item.Email == email).FirstOrDefault();
            if (tempModel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public UserEmailIdDTO setNewUserEmail(string newUserEmail, string userID)
        {
            UserEmailIdDTO userEmailIdDTO = new(userID);
            var userToPatch = _context.UserList?.Where(user => user.Id == userID).FirstOrDefault();
            var probablyExistingUser = _context.UserList?.Where(user => user.Email == newUserEmail).FirstOrDefault();
            if (probablyExistingUser == null)
            {
                updateUserInDB();
                userEmailIdDTO.NewEmail = newUserEmail;
            }
            else
            {
                userEmailIdDTO.NewEmail = null;
            }
            return userEmailIdDTO;

            void updateUserInDB()
            {
                userToPatch.Email = newUserEmail;
                userToPatch.NormalizedEmail = newUserEmail.ToUpper();
                userToPatch.UserName = newUserEmail;
                userToPatch.NormalizedUserName = newUserEmail.ToUpper();

                userToPatch = null;

                _context.SaveChanges();
            }
        }

        public User? GetUserById(string Id)
        {
            return _context.UserList?.Where(user => user.Id == Id).FirstOrDefault();
        }
    }
}
