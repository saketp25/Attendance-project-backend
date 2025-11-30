namespace Attendance_Manager.API.Controllers.Authentication.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
