using HoneyStore.Api.ViewModels;
using HoneyStore.BusinessLogic.Interfaces;
using HoneyStore.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HoneyStore.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public UsersController(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usersFromDb = await _userManager.Users.ToListAsync();

            var users = usersFromDb.Select(u => new UserModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                RoleName = _userService.GetUserRolesAsync(u.Id).Result.Select(r => r.Name).FirstOrDefault()
            });

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=> u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok(user);
        }

        [HttpPut("role/{role}")]
        public async Task<IActionResult> ChangeUserRole([FromBody] UserModel user, string role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var userFromDb = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (userFromDb == null)
            {
                return BadRequest("User does not exists.");
            }

            var oldRoleName = user.RoleName;
            if (oldRoleName != role)
            {
                await _userManager.RemoveFromRoleAsync(userFromDb, oldRoleName);
                await _userManager.AddToRoleAsync(userFromDb, role);
            }

            return NoContent();
        }
    }
}
