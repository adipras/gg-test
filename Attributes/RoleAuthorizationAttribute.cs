using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using gg_test.Models;
using gg_test.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace gg_test.Attributes
{
    public class RoleAuthorizationAttribute : TypeFilterAttribute
    {
        public RoleAuthorizationAttribute() : base(typeof(RoleAuthorizationFilter))
        {
        }
    }

    public class RoleAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleAuthorizationFilter(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userIdInt);
            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.Items["CurrentUser"] = user;
        }
    }
}
