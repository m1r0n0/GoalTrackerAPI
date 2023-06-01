using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Exceptions;
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
        private readonly UserManager<User?> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<User?> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            IMapper mapper
            ) : base(httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Register(UserDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            if (_accountService.CheckGivenEmailForExistingInDB(model.Email))
                return Conflict(model);
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(model);
            model.Id = _accountService.GetUserIDFromUserEmail(model.Email).UserId;
            await _signInManager.SignInAsync(user, false);
            return Ok(model);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] UserDTO model)
        {
            var result =
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!result.Succeeded) return BadRequest(model);
            UserEmailIdDTO emailIdDTO = _accountService.GetUserIDFromUserEmail(model.Email);
            model.Id = emailIdDTO.UserId;
            return Ok(model);
        }

        [HttpGet]
        public UserEmailIdDTO GetUserId(string userEmail)
        {
            return _accountService.GetUserIDFromUserEmail(userEmail);
        }

        [HttpGet]
        public UserEmailIdDTO GetUserEmail(string userId)
        {
            return _accountService.GetUserEmailFromUserID(userId);
        }

        [HttpGet]
        public CheckExistingEmailDTO CheckEmailExisting(string email)
        {
            return new(email, _accountService.CheckGivenEmailForExistingInDB(email));
        }

        [HttpPatch]
        public UserEmailIdDTO ChangeUserEmail(UserEmailIdDTO model)
        {
            return _accountService.setNewUserEmail(model.NewEmail!, model.UserId);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeUserPassword(UserPasswordIdDTO model)
        {
            try
            {
                User? user = _accountService.GetUserById(model.UserId);
                if (user is null) throw new NotFoundException();
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(model);
                }

                return BadRequest(model);
            }
            catch (NotFoundException ex)
            {
                return NotFound(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                return Ok(_accountService.GetUserById(id));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(id);
            }
            
        }
    }
}