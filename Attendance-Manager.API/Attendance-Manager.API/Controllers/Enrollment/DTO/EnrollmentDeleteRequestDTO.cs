namespace Attendance_Manager.API.Controllers.Enrollment.DTO
{
    public class EnrollmentDeleteRequestDTO
    {
        public int ClassId { get; set; }
        public List<int> StudentList { get; set; } = new List<int>();
    }
}
