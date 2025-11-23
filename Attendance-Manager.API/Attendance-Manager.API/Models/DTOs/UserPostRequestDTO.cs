namespace Attendance_Manager.API.Models.DTOs
{
    public class UserPostRequestDTO
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; }  = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
