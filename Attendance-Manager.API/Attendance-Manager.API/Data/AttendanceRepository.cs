
namespace Attendance_Manager.API.Data
{
    public class AttendanceRepository
    {
        private readonly AppDBContext _appDBContext;

        public AttendanceRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public User GetUser(int id)
        {
            return _appDBContext.Users.Find(id);
        }
    }
}
