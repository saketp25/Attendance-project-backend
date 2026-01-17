namespace Attendance_Manager.API.Controllers.Class.DTO
{
    public class ClassPatchRequestDTO
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
    }
}
