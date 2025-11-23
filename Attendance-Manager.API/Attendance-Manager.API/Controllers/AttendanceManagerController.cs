using Attendance_Manager.API.Data;
using Attendance_Manager.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("2.0")]
    public class AttendanceManagerController : ControllerBase
    {
        private readonly ILogger<AttendanceManagerController> _logger;
        private readonly AppDBContext _dbContext;

        public AttendanceManagerController(ILogger<AttendanceManagerController> logger,AppDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("AddTeacher")]
        [Authorize(Roles = "Admin")]
        public void AddTeacher([FromBody] UserPostRequestDTO dto)
        {
            var newTeacher = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            _dbContext.Add(newTeacher);
            _dbContext.SaveChanges();
        }
    }
}
