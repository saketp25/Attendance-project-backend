using Attendance_Manager.API.Controllers.Authentication.DTO;
using Attendance_Manager.API.Data;
using Attendance_Manager.API.JwtAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers.Authentication
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly AppDBContext _dbContext;

        public AuthController(UserManager<ApplicationUser> userManager,
                              //SignInManager<ApplicationUser> signInManager,
                              JwtTokenService jwtTokenService,
                              AppDBContext dBContext)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _dbContext = dBContext;
        }

        
        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var identityUser = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FullName = registerDTO.Fullname
            };

            var identityUserResult = await _userManager.CreateAsync(identityUser, registerDTO.Password);
            if (!identityUserResult.Succeeded)
                return BadRequest(identityUserResult.Errors);

            var roleResult = await _userManager.AddToRoleAsync(identityUser, registerDTO.Role?? "Student");
            if(!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            var newUser = new Attendance_Manager.API.Data.User
            {
                UserName = registerDTO.Fullname,
                Email = registerDTO.Email,
                Password = identityUser.PasswordHash,
                Role = registerDTO.Role ?? "Student",
                IdentityUserId = identityUser.Id,
            };

            var newUserResult = await _dbContext.AddAsync(newUser);
            if (newUserResult == null)
                return BadRequest("unable to Add new User");
            
            _dbContext.SaveChanges();

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
