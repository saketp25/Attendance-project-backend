namespace Attendance_Manager.API.Controllers.Class.DTO
{
    public class ClassPostRequestDTO
    {
        public string ClassName { get; set; } = string.Empty;
        public int TeacherId { get; set; }
    }
}
