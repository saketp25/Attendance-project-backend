using Attendance_Manager.API.Controllers.User.DTO;
using Attendance_Manager.API.Data;

namespace Attendance_Manager.API.Controllers.User
{
    public class UserService
    {
        private readonly AttendanceRepository _attendanceRepository;
        public UserService(AttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public UserGetResponseDTO GetUser(int id)
        {
            var user = _attendanceRepository.GetUser(id);
            
            if (user == null)
                return null;

            var userResponse = new UserGetResponseDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                IdentityUserId = user.IdentityUserId,
            };

            return userResponse;
        }

    }
}

