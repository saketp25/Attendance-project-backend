
using Attendance_Manager.API.Controllers.Class.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Attendance_Manager.API.Controllers.Class
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class ClassController
    {
        private readonly ClassService _classService;

        public ClassController(ClassService classService)
        {
            _classService = classService;
        }

        [HttpPost]
        [Authorize(Roles="Teacher")]
        [Route("addclass")]
        public async Task<IActionResult> AddClass([FromBody] ClassPostRequestDTO classPostRequestDTO)
        {
            var result = await _classService.AddClass(classPostRequestDTO);

            if (result == null)
                return new BadRequestObjectResult("Failed to Add new Class");

            return new OkObjectResult(result);
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        [Route("GetClassByTeacherId/{teacherId}")]
        public async Task<IActionResult> GetClassByTeacherId(int teacherId)
        {
            var result = await _classService.GetClassByTeacherId(teacherId);

            if (result == null)
                return new BadRequestObjectResult("Failed to Fetch Class");

            return new OkObjectResult(result);
        }

        [HttpDelete]
        [Authorize(Roles = "Teacher")]
        [Route("deleteClassById/{id}")]
        public async Task<IActionResult> DeleteClassById(int id)
        {
            var result = await _classService.DeleteClassById(id);

            return new OkObjectResult(result);
        }

        [HttpPatch]
        [Authorize(Roles ="Teacher")]
        [Route("updateClassById")]
        public async Task<IActionResult> UpateClassById([FromBody] ClassPatchRequestDTO classPatchRequestDTO)
        {
            var result = await _classService.UpdateClassById(classPatchRequestDTO);
            return new OkObjectResult(result);
        }
    }
}
