using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HoneyStore.Api.ViewModels;
using HoneyStore.DataAccess.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HoneyStore.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
            {
                return BadRequest();
            }

            var user= await _userManager.FindByNameAsync(model.Email);
                


            var response = new
            {
                access_token = GetToken(user),
                user = new UserModel
                {
                    Id = user.Id,
                    RoleName = _userManager.GetRolesAsync(user).Result.Single(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }
            };

            return Ok(response);

        }

        [Authorize]
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await _userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
            );
            return Ok(GetToken(user));

        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = new User
            {
                //TODO: Use Automapper instaed of manual binding
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(GetToken(user));
            }
            else
            {
                return BadRequest(identityResult.Errors);
            }
        }

        private string GetToken(User user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSettings:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_configuration.GetValue<int>("TokenSettings:Lifetime")),
                audience: _configuration.GetValue<string>("TokenSettings:Audience"),
                issuer: _configuration.GetValue<string>("TokenSettings:Issuer")
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
