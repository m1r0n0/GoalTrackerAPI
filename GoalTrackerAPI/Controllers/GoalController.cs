using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ShortenUrlWebApi.Controllers;

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
        public IActionResult CreateGoal(GoalCreationDTO goal)
        {
            var mappedGoal = _mapper.Map<Goal>(goal);
            _goalService.CreateGoal(goal);
            return Ok(goal);
        }

        /*        [HttpGet]
                public IActionResult GetAllGoals()
                {

                }*/
    }
}
