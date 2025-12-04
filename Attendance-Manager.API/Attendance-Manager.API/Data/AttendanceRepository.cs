
namespace Attendance_Manager.API.Data
{
    public class AttendanceRepository
    {
        private readonly AppDBContext _appDBContext;

        public AttendanceRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<User?> GetUser(int id)
        {
            return await _appDBContext.Users.FindAsync(id);
        }
    }
}
