using AutoMapper;
using BusinessLayer.DTO.GoalCreationDTO;
using BusinessLayer.DTO.GoalsGetting;
using BusinessLayer.Interfaces;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;

namespace GoalTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GoalController : AppController
    {
        private readonly GoalContext _context;
        private readonly IMapper _mapper;
        private readonly IGoalService _goalService;

        public GoalController(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            GoalContext context,
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
        public async Task<GoalsListDTO> GetAllGoals()
        {
            return await _goalService.GetGoals();
        }
    }
}
