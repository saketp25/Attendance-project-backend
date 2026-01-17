
using Attendance_Manager.API.Controllers.Session.DTO;
using Attendance_Manager.API.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Attendance_Manager.API.Controllers.Session
{
    public class SessionService
    {
        public readonly AttendanceRepository _attendanceRepository;

        public SessionService(AttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<bool> AddSession(SessionPostRequestDTO sessionPostRequestDTO)
        {
            var newSession = new Attendance_Manager.API.Data.Session
            {
                SessionName = sessionPostRequestDTO.SessionName,
                ClassId = sessionPostRequestDTO.ClassId,
                SessionSchedule = sessionPostRequestDTO.SessionSchedule
            };

            var result = await _attendanceRepository.AddSession(newSession);
            return result;
        }

        public async Task<List<SessionGetResponseDTO>?> GetSessionsByClassId(int classId)
        {
            var result = await _attendanceRepository.GetSessionsByClassId(classId);
            if(result == null)
                return null;

            var sessionListResult = result.Select(s => new SessionGetResponseDTO
            {
                SessionId = s.SessionId,
                SessionName = s.SessionName,
                ClassId = s.ClassId,
                SessionSchedule = s.SessionSchedule
            }).ToList();

            return sessionListResult;
        }

        public async Task<Attendance_Manager.API.Data.Session?> GetSessionById(int id)
        {
            var result = await _attendanceRepository.GetSessionById(id);
            return result;
        }

        public async Task<bool> DeleteSessionById(int id)
        {
            var sessionToDelete = await GetSessionById(id);
            if (sessionToDelete == null)
                return false;

            var result = await _attendanceRepository.DeleteSessionById(sessionToDelete);
            return result;
        }

        public async Task<bool> UpdateSessionById(SessionPatchRequestDTO sessionPatchRequestDTO)
        {
            var sessionToUpdate = await GetSessionById(sessionPatchRequestDTO.SessionId);
            if (sessionToUpdate == null)
                return false;

            sessionToUpdate.SessionName = sessionPatchRequestDTO.SessionName;
            sessionToUpdate.SessionSchedule = sessionPatchRequestDTO.SessionSchedule;

            var result = await _attendanceRepository.UpdateSessionById(sessionToUpdate);
            return result;
        }
    }
}
