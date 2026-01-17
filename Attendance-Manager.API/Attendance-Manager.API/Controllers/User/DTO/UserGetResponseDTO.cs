using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Attendance_Manager.API.Controllers.User.DTO
{
    public class UserGetResponseDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string IdentityUserId { get; set; } = string.Empty;
    }
}
