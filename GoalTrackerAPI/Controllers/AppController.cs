using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShortenUrlWebApi.Controllers
{
    [ApiController]
    public abstract class AppController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected string? GetUserIdFromClaims()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
