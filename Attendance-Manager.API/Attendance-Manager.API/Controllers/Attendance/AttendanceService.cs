using Attendance_Manager.API.Controllers.Attendance.DTO;
using Attendance_Manager.API.Data;

namespace Attendance_Manager.API.Controllers.Attendance
{
    public class AttendanceService
    {
        private readonly AttendanceRepository _attendanceRepository;

        public AttendanceService(AttendanceRepository attendanceRespository)
        {
            _attendanceRepository = attendanceRespository;
        }

        public async Task<bool> SaveAttedanceBySessionID(int sessionId, IEnumerable<AttedancePostRequestDTO> attedancePostRequestDTOs)
        {
            var studentIds = attedancePostRequestDTOs.Select(c => c.StudentId).ToList();

            var validateStudentIdList = await  _attendanceRepository.ValidateStudentIdsExist(studentIds);
            if (validateStudentIdList == null)
                return false;

            var validatedStudents = attedancePostRequestDTOs.Where(c => validateStudentIdList.Contains(c.StudentId)).Select(c => new AttendanceRecord
            {
                SessionId = sessionId,
                StudentId = c.StudentId,
                Status = c.Status
            }).ToList();

            var result = await _attendanceRepository.SaveAttedanceBySessionID(sessionId, validatedStudents);
            if (result == false)
                return false;

            return result;
        }

        public async Task<IEnumerable<StudentAttendanceGetResponseDTO>?> GetAttendanceBySessionId(int sessionId)
        {
            var attendanceRecords = await _attendanceRepository.GetAttendanceBySessionId(sessionId);
            if (attendanceRecords == null)
                return null;

            var result = attendanceRecords.Select(e => new StudentAttendanceGetResponseDTO
            {
                StudentId = e.StudentId,
                Status = e.Status
            });

            return result;
        }

        public async Task<IEnumerable<SessionAttendanceGetResponseDTO>?> GetAttendanceByStudentIdClassID(int studentId, int classId)
        {
            //Fetch all the sessions for the given class 
            var sessionList = await _attendanceRepository.FindSessionsByClassId(classId);
            if (sessionList == null)
                return null;

            var sessionIds = sessionList.Select(s => s.SessionId);

            var records = await _attendanceRepository.GetAttendanceByStudentIdSessionID(studentId, sessionIds);
            if (records == null)
                return null;

            // Create a dictionary for O(1) lookup instead of O(n) FirstOrDefault
            var sessionDictionary = sessionList.ToDictionary(s => s.SessionId, s => s.SessionName);

            var result = records.Select(r => new SessionAttendanceGetResponseDTO
            {
                SessionId = r.SessionId,
                SessionName = sessionDictionary[r.SessionId],
                Status = r.Status
            });

            return result;
        }
    }
}
