
using Attendance_Manager.API.Controllers.Class.DTO;
using Microsoft.EntityFrameworkCore;

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

        // Class Methods
        public async Task<Class> AddClass(Class newClass)
        {
            var entry = await _appDBContext.Classes.AddAsync(newClass);
            await _appDBContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<List<Class>> GetClassByTeacherId(int teacherId)
        {
            var result = await _appDBContext.Classes.Where(c => c.TeacherId == teacherId).ToListAsync();

            return result;
        }

        public async Task<bool> DeleteClassById(int classId)
        {
            var classToDelete = await _appDBContext.Classes.FirstOrDefaultAsync(c => c.ClassId == classId);

            if (classToDelete == null)
                return false;

            _appDBContext.Classes.Remove(classToDelete);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateClassById(Class classToUpdate)
        {
            _appDBContext.Classes.Update(classToUpdate);
            await _appDBContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<Class> GetClassById(int classId)
        {
            var result = await _appDBContext.Classes.FirstOrDefaultAsync(c => c.ClassId == classId);
            return result!;
        }
    }
}
