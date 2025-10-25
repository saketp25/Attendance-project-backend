namespace Attendance_Manager.API.Models.DTOs
{
    public class TeacherPostRequestDTO
    {
        public string TeacherName { get; set; } = string.Empty;

        public string email { get; set; }  = string.Empty;

        public string password { get; set; } = string.Empty;
    }
}
