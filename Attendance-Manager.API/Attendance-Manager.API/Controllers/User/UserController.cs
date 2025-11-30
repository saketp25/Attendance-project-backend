using Attendance_Manager.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers.User
{
    [ApiController]
    [Authorize]
    [ApiVersion("2.0")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDBContext _dbContext;
        
        public UserController(ILogger<UserController> logger,AppDBContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var response = _dbContext.Users.Find(id);

            if (response==null)
                return null;

            return Ok(response);
        }
    }
}
