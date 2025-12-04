using Attendance_Manager.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    [ApiVersion("2.0")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;
        
        public UserController(ILogger<UserController> logger,UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
                return NotFound("User Not Found");

            return Ok(user);
        }
    }
}
