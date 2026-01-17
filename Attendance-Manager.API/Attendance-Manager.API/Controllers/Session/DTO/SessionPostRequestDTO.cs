namespace Attendance_Manager.API.Controllers.Session.DTO
{
    public class SessionPostRequestDTO
    {
        public string SessionName { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public DateTime SessionSchedule { get; set; }
    }
}
