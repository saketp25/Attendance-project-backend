namespace Attendance_Manager.API.Controllers.Session.DTO
{
    public class SessionGetResponseDTO
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public DateTime SessionSchedule { get; set; }
    }
}
