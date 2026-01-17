namespace Attendance_Manager.API.Controllers.Enrollment.DTO
{
    public class EnrollmentPostRequestDTO
    {
        public int ClassId { get; set; }
        public List<int> StudentList { get; set; } = new List<int>();
    }
}
