using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class GoalService : IGoalService
    {
        private readonly IConfiguration _configuration;
        private readonly DataAccessLayer.Data.GoalContext _context;
        //private readonly IMapper _mapper;

        public GoalService(DataAccessLayer.Data.GoalContext context,
            IConfiguration configuration
            //IMapper mapper
            )
        {
            _context = context;
            _configuration = configuration;
            //_mapper = mapper;
        }

        public async Task<Goal> CreateGoal(Goal goal)
        {
            _context.GoalList.Add(goal);
            await _context.SaveChangesAsync();
            return goal;
        }
    }
}
