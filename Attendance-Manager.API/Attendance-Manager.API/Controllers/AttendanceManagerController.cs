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
        [Route("AddTeacher")]
        public void AddTeacher([FromBody] TeacherPostRequestDTO dto)
        {
            var newTeacher = new Teacher
            {
                TeacherName = dto.TeacherName,
                TeacherEmail = dto.email,
                TeacherPassword = dto.password
            };

            _dbContext.Add(newTeacher);
            _dbContext.SaveChanges();
        }

    }
}
