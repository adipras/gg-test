using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using gg_test.Attributes;
using gg_test.Models;
using gg_test.Data;

namespace gg_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [RoleAuthorization]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var currentUser = (User)HttpContext.Items["CurrentUser"]!;

            IQueryable<UserDto> query = _context.Users
                 .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Department = u.Department,
                    Role = u.Role
                });

            switch (currentUser.Role)
            {
                case Role.Admin:
                    // Admin can see all users
                    break;
                case Role.Manager:
                    // Manager can only see users in their department
                    query = query.Where(u => u.Department == currentUser.Department);
                    break;
                case Role.Employee:
                    // Employee can only see their own data
                    query = query.Where(u => u.Id == currentUser.Id);
                    break;
                default:
                    return Forbid();
            }

            var users = await query.ToListAsync();
            return Ok(users);
        }
    }
}
