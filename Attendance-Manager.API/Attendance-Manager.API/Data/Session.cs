namespace Attendance_Manager.API.Data
{
    public class Session
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;
        public DateTime SessionSchedule { get; set; }
    }
}
