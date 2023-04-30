using AutoMapper;
using BusinessLayer.DTOs.GoalCreationDTO;
using BusinessLayer.DTOs.GoalsGettingDTO;
using BusinessLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoalTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GoalController : AppController
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IGoalService _goalService;

        public GoalController(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            ApplicationContext context,
            IGoalService goalService) : base(httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _goalService = goalService;
        }

        [HttpPut]
        public async Task<IActionResult> CreateGoal(GoalForCreationDTO goal)
        {
            await _goalService.CreateGoal(goal);
            return Ok(goal);
        }

        [HttpGet]
        public async Task<GoalsListForGettingDTO> GetGoalsOfParticularUser(string userId)
        {
            return await _goalService.GetGoalsForUser(userId);
        }

        [HttpPatch]
        public async Task<IActionResult> EditGoal(GoalForCreationDTO goal)
        {
            Goal editedGoal = await _goalService.EditGoal(goal);
            return Ok(editedGoal);
        }
    }
}
