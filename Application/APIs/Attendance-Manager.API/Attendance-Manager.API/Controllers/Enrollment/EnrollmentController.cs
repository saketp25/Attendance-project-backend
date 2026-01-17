using Attendance_Manager.API.Controllers.Enrollment.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers.Enrollment
{

    [ApiController]
    [Authorize]
    [Route("[Controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;
        public EnrollmentController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [Route("AddStudentsToClass")]
        public async Task<IActionResult> AddStudentsToClass([FromBody] EnrollmentPostRequestDTO enrollmentPostRequestDTO)
        {
            var result = await _enrollmentService.AddStudentsToClass(enrollmentPostRequestDTO);
            if (result == null)
                return BadRequest("Failed to Enroll student");

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher")]
        [Route("RemoveStudentFromClass")]
        public async Task<IActionResult> RemoveStudentFromClass([FromBody] EnrollmentDeleteRequestDTO enrollmentDeleteRequestDTO)
        {
            var result = await _enrollmentService.RemoveStudentFromClass(enrollmentDeleteRequestDTO);
            if (result == null)
                return BadRequest("Failed to remove students");

            return Ok(result);
        }

    }
}
