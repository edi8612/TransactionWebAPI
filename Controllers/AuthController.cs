using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransactionWebAPI.Models;
using TransactionWebAPI.Models.Dto;

namespace TransactionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //[Authorize]
        [HttpGet("status")]
        public IActionResult GetAuthStatus()
        {

            var isAuthed = User?.Identity?.IsAuthenticated == true;
            if (isAuthed)
                return Ok(new { authenticated = true, user = User.Identity!.Name });

            return Unauthorized(new { authenticated = false });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, dto.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
                return Ok(new { message = "Login successful." });

            if (result.IsNotAllowed)
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "Not allowed to sign in." });

            if (result.IsLockedOut)
                return StatusCode(423, new { message = "Account locked." });

            return Unauthorized(new { message = "Invalid credentials." });
        }



        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupDTO dto)
        {
            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                
                await _signInManager.SignInAsync(user, isPersistent: true);
                return Ok(new { message = "Registration successful." });
            }

            var errors = result.Errors?.Select(e => e.Description).ToList() ?? new();
            var isDuplicate = result.Errors?.Any(e =>
                e.Code.Contains("Duplicate", StringComparison.OrdinalIgnoreCase)) == true;

            if (isDuplicate)
                return Conflict(new { message = "Email already exists.", errors });

            return BadRequest(new { message = "Registration failed.", errors });
        }

        
        

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out.");
        }
  




    }
}
