using Attendance_Manager.API.Controllers.Attendance.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers.Attendance
{
    [ApiController]
    [Authorize]
    [Route("[Controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceService _attendanceService;

        public AttendanceController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [Route("SaveAttedanceBySessionID/{sessionId}")]
        public async Task<IActionResult> SaveAttedanceBySessionID(int sessionId, [FromBody] List<AttedancePostRequestDTO> attedancePostRequestDTOs)
        {
            var result = await _attendanceService.SaveAttedanceBySessionID(sessionId,attedancePostRequestDTOs);
            if (result == false)
                return BadRequest("Failed to Save Attendance");

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        [Route("GetAttendanceBySessionId/{sessionId}")]
        public async Task<IActionResult> GetAttendanceBySessionId(int sessionId)
        {
            var result = await _attendanceService.GetAttendanceBySessionId(sessionId);
            if (result == null)
                return BadRequest("Failed to Fetch Attendance");

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher,Student")]
        [Route("GetAttendanceByStudentIdClassID/{studentId}/{classId}")]
        public async Task<IActionResult> GetAttendanceByStudentIdClassId(int studentId,int classId)
        {
            var result = await _attendanceService.GetAttendanceByStudentIdClassID(studentId,classId);
            if (result == null)
                return BadRequest("Failed to Fetch Attendance");

            return Ok(result);
        }

    }
}
