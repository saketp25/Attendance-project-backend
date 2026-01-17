namespace Attendance_Manager.API.Controllers.Attendance.DTO
{
    public class SessionAttendanceGetResponseDTO
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public bool Status { get; set; }
    }
}
