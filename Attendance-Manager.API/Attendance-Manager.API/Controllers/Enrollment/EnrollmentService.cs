using Attendance_Manager.API.Controllers.Enrollment.DTO;
using Attendance_Manager.API.Data;

namespace Attendance_Manager.API.Controllers.Enrollment
{
    public class EnrollmentService
    {
        private readonly AttendanceRepository _attendanceRepository;
        
        public EnrollmentService(AttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        
        public async Task<List<int>?> AddStudentsToClass(EnrollmentPostRequestDTO enrollmentPostRequestDTO)
        { 
            // check whether input studentIds are valid user Ids
            var validStudentIds = await _attendanceRepository.ValidateStudentIdsExist(enrollmentPostRequestDTO.StudentList);
            if (validStudentIds == null)
                return null;

            // check validStudent Ids are not present in enrollmentment table (not already enrollled for given class)
            var checkedStudentIds = await _attendanceRepository.checkValidStudentIdsAlreadyExitOrNot(validStudentIds, enrollmentPostRequestDTO.ClassId,"ADD");
            if (checkedStudentIds == null)
                return null;

            // Add studentIds which are valid and not already exit in enrollment table with given classId
            var result = await _attendanceRepository.AddStudentsToClass(checkedStudentIds, enrollmentPostRequestDTO.ClassId);
            if (result == false)
                return null;

            return checkedStudentIds;
        }

        public async Task<List<int>?> RemoveStudentFromClass(EnrollmentDeleteRequestDTO enrollmentDeleteRequestDTO)
        {
            // check whether input studentIds are valid user Ids
            var validStudentIds = await _attendanceRepository.ValidateStudentIdsExist(enrollmentDeleteRequestDTO.StudentList);
            if (validStudentIds == null)
                return null;

            // check validStudent Ids are present in enrollmentment table (enrollled for given class)
            var checkedStudentIds = await _attendanceRepository.checkValidStudentIdsAlreadyExitOrNot(validStudentIds, enrollmentDeleteRequestDTO.ClassId,"REMOVE");
            if (checkedStudentIds == null)
                return null;

            // Remove studentIds which are valid and exits in enrollment table with given classId
            var result = await _attendanceRepository.RemoveStudentFromClass(checkedStudentIds, enrollmentDeleteRequestDTO.ClassId);
            if (result == false)
                return null;

            return checkedStudentIds;
        }
    }
}
