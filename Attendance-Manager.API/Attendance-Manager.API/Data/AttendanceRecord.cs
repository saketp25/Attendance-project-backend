namespace Attendance_Manager.API.Data
{
    public class AttendanceRecord
    {
        public int AttendanceId { get; set; }
        public int SessionId { get; set; }
        public int StudentId { get; set; }
        public bool status { get; set; }
    }
}
