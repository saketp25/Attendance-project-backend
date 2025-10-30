namespace Attendance_Manager.API.Data
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentPassword { get; set; } = string.Empty;
    }
}
