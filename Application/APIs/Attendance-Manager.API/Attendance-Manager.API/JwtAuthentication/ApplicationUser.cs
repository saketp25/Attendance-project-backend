using Microsoft.AspNetCore.Identity;

namespace Attendance_Manager.API.JwtAuthentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
