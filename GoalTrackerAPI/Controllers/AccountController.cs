using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoalTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : AppController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService
            ) : base(httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [HttpPut]
        public async Task<IActionResult> Register(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.CheckGivenEmailForExistingInDB(model.Email))
                {
                    return Conflict(model);
                }
                User user = new User { Email = model.Email, UserName = model.Email, Name = model.Name, };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserEmailIdDTO emailIdDTO = _accountService.GetUserIDFromUserEmail(model.Email);
                    model.Id = emailIdDTO.UserId;
                    await _signInManager.SignInAsync(user, false);
                    return Ok(model);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest(model);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] UserDTO model)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                UserEmailIdDTO emailIdDTO = _accountService.GetUserIDFromUserEmail(model.Email);
                model.Id = emailIdDTO.UserId;
                return Ok(model);
            }
            else
            {
                ModelState.AddModelError("", "Incorrect login and (or) password");
            }
            return BadRequest(model);
        }

        [HttpGet]
        public UserEmailIdDTO GetUserID(string userEmail)
        {
            return _accountService.GetUserIDFromUserEmail(userEmail);
        }

        [HttpGet]
        public UserEmailIdDTO GetUserEmail(string userID)
        {
            return _accountService.GetUserEmailFromUserID(userID);
        }

        [HttpGet]
        public CheckExistingEmailDTO CheckEmailExisting(string email)
        {
            return new(email, _accountService.CheckGivenEmailForExistingInDB(email));
        }

        [HttpPatch]
        public UserEmailIdDTO ChangeUserEmail(UserEmailIdDTO model)
        {
            return _accountService.setNewUserEmail(model.NewEmail, model.UserId);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeUserPassword(UserPasswordIdDTO model)
        {
            User user = _accountService.GetUserById(model.UserId);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest(model);
            };
        }
    }
}