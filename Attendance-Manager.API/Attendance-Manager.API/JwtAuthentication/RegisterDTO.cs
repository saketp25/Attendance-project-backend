namespace Attendance_Manager.API.JwtAuthentication
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
