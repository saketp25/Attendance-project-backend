using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Attendance_Manager.API.Controllers.Session.DTO
{
    public class SessionPatchRequestDTO
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; } = String.Empty;
        public int ClassId { get; set; }
        public DateTime SessionSchedule { get; set; }
    }
}
