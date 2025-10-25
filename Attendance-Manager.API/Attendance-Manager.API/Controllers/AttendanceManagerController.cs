using Attendance_Manager.API.Data;
using Attendance_Manager.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers
{
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
        [Route("AddUser")]
        public void AddUser([FromBody] PostRequestDTO dto)
        {
            var newUser = new Users
            {
                UserName = dto.name
            };

            _dbContext.Add(newUser);
            _dbContext.SaveChanges();
        }

        [HttpPost]
        [Route("AddTeacher")]
        public void AddTeacher([FromBody] TeacherPostRequestDTO dto)
        {
            var newTeacher = new Teachers
            {
                TeacherName = dto.TeacherName,
                email = dto.email,
                password = dto.password
            };

            _dbContext.Add(newTeacher);
            _dbContext.SaveChanges();
        }

    }
}
