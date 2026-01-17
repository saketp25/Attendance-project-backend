namespace Attendance_Manager.API.Controllers.Class.DTO
{
    public class ClassGetResponseDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int TeacherId { get; set; }
    }
}
