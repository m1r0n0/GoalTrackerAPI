using AutoMapper;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Services
{
    public class GoalService : IGoalService
    {
        private readonly IConfiguration _configuration;
        private readonly DataAccessLayer.Data.GoalContext _context;
        private readonly IMapper _mapper;

        public GoalService(DataAccessLayer.Data.GoalContext context,
            IConfiguration configuration,
            IMapper mapper
            )
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<GoalCreationDTO> CreateGoal(GoalCreationDTO goal)
        {
            var mappedGoal = _mapper.Map<Goal>(goal);
            int i = 0;
            _context.GoalList.Add(_mapper.Map<Goal>(goal));
            await _context.SaveChangesAsync();
            return goal;
        }
    }
}
