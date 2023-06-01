using AutoMapper;
using BusinessLayer.DTOs.UserDTOs;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataAccessLayer.Data.ApplicationContext _context;
        private readonly IMapper _mapper;
        public AccountService(DataAccessLayer.Data.ApplicationContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            User? userToPatch = _context.UserList?.Where(user => user.Id == userID).FirstOrDefault();
            User? probablyExistingUser = _context.UserList?.Where(user => user.Email == newUserEmail).FirstOrDefault();
            if (probablyExistingUser == null)
            {
                UpdateUserInDB();
                userEmailIdDTO.NewEmail = newUserEmail;
            }
            else
            {
                userEmailIdDTO.NewEmail = null;
            }
            return userEmailIdDTO;

            void UpdateUserInDB()
            {
                userToPatch!.Email = newUserEmail;
                userToPatch.NormalizedEmail = newUserEmail.ToUpper();
                userToPatch.UserName = newUserEmail;
                userToPatch.NormalizedUserName = newUserEmail.ToUpper();

                userToPatch = null;

                _context.SaveChanges();
            }
        }

        public async Task<User> GetUserById(string Id)
        {
            User? user = await _context.UserList?.Where(user => user.Id == Id).FirstOrDefaultAsync();
            if (user is null) throw new NotFoundException();
            return user;
        }
    }
}
