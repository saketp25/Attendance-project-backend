using Attendance_Manager.API.Controllers.Session.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Manager.API.Controllers.Session
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class SessionController : ControllerBase
    {
        public readonly SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        [Route("AddSession")]
        public async Task<IActionResult> AddSession([FromBody] SessionPostRequestDTO sessionPostRequestDTO)
        {
            var result = await _sessionService.AddSession(sessionPostRequestDTO);
            if (result == false)
                return BadRequest("Failed to Create Session");

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        [Route("GetSessionByClassId/{classId}")]
        public async Task<IActionResult> GetSessionsByClassId(int classId)
        {
            var result = await _sessionService.GetSessionsByClassId(classId);

            if (result == null)
                return BadRequest("Failed to Fetch Sessions");

            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher")]
        [Route("DeleteSessionById/{id}")]
        public async Task<IActionResult> DeleteSessionById(int id)
        {
            var result = await _sessionService.DeleteSessionById(id);
            if (result is false)
                return BadRequest("Failed to Delete Session");

            return Ok(result);
        }

        [HttpPatch]
        [Authorize(Roles = "Teacher")]
        [Route("UpdateSessionById")]
        public async Task<IActionResult> UpdateSessionById([FromBody] SessionPatchRequestDTO sessionPatchRequestDTO)
        {
            var result = await _sessionService.UpdateSessionById(sessionPatchRequestDTO);

            if (result is false)
                return BadRequest("Failed to update Session");

            return Ok(result);
        }

    }
}
