using Attendance_Manager.API.JwtAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.JwtAuthentication
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;
        public AuthController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FullName = registerDTO.Fullname
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, registerDTO.Role?? "Student");
            if(!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(loginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user == null)
                return Unauthorized(new { Message = "Invalid Credentials" });
            
            var userPassword = await _userManager.CheckPasswordAsync(user , loginDTO.Password);
            if(!userPassword)
                return Unauthorized(new { Message = "Invalid Credentials" });

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenService.CreateToken(user, roles);

            return Ok(new { Token = token });
        }

    }
}
