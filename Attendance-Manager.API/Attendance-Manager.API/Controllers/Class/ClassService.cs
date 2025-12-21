using Attendance_Manager.API.Controllers.Class.DTO;
using Attendance_Manager.API.Data;

namespace Attendance_Manager.API.Controllers.Class
{
    public class ClassService
    {
        public readonly AttendanceRepository _attendanceRepository;

        public ClassService(AttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<Attendance_Manager.API.Data.Class> AddClass(ClassPostRequestDTO classPostRequestDTO)
        {
            var newClass = new Attendance_Manager.API.Data.Class
            {
                ClassName= classPostRequestDTO.ClassName,
                TeacherId= classPostRequestDTO.TeacherId
            };

            var result = await _attendanceRepository.AddClass(newClass);

            return result;
        }

        public async Task<List<ClassGetResponseDTO>> GetClassByTeacherId(int teacherId)
        {
            var result = await _attendanceRepository.GetClassByTeacherId(teacherId);

            var classListResult = result.Select(c => new ClassGetResponseDTO
            {
                ClassId = c.ClassId,
                ClassName = c.ClassName,
                TeacherId = c.TeacherId
            }).ToList();

            return classListResult;
        }

        public async Task<bool> DeleteClassById(int classId)
        {
            return await _attendanceRepository.DeleteClassById(classId);
        }

        public async Task<bool> UpdateClassById(ClassPatchRequestDTO classPatchRequestDTO)
        {
            var getClass = await _attendanceRepository.GetClassById(classPatchRequestDTO.ClassId);
            if (getClass == null)
                return false;

            getClass.ClassName = classPatchRequestDTO.ClassName;

            var result = await _attendanceRepository.UpdateClassById(getClass);
            return result;
        }

        public async Task<Attendance_Manager.API.Data.Class> GetClassById(int classId)
        {
            var result = await _attendanceRepository.GetClassById(classId);
            return result;
        }

    }
}
