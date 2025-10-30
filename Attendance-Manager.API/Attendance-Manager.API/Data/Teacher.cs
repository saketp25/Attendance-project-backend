namespace Attendance_Manager.API.Data
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string TeacherEmail { get; set; } = string.Empty;
        public string TeacherPassword { get; set; } = string.Empty;
    }
}
