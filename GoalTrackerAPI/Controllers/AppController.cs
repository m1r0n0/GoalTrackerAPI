using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace GoalTrackerAPI.Controllers
{
    [ApiController]
    public abstract class AppController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected AppController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected string? GetUserIdFromClaims()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
