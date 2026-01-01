using Attendance_Manager.API.Controllers.Class.DTO;
using Attendance_Manager.API.Controllers.Enrollment.DTO;
using Microsoft.AspNetCore.Mvc;
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

        // Session Methods

        public async Task<bool> AddSession(Session session)
        {
            var entry = await _appDBContext.Sessions.AddAsync(session);
            await _appDBContext.SaveChangesAsync();
            return entry?.Entity != null;
        }

        public async Task<Session?> GetSessionById(int id)
        {
            var result = await  _appDBContext.Sessions.FirstOrDefaultAsync(s => s.SessionId == id);
            return result;
        }

        public async Task<List<Session>> GetSessionsByClassId(int classId)
        {
            var result = await _appDBContext.Sessions.Where(s => s.ClassId == classId).ToListAsync();
            return result;
        }

        public async Task<bool> DeleteSessionById(Session session)
        {
            var result =  _appDBContext.Sessions.Remove(session);

            await _appDBContext.SaveChangesAsync();

            return result.Entity != null ? true:false;
        }

        public async Task<bool> UpdateSessionById(Session sessionToUpdate)
        {
           _appDBContext.Sessions.Update(sessionToUpdate);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

        // Enrollment Methods

        public async Task<List<int>?> ValidateStudentIdsExist(List<int> studentIds)
        {
            var result = new List<int>();

            foreach (int studentId in studentIds)
            {
                var user = await _appDBContext.Users.FirstOrDefaultAsync(s => s.UserId == studentId);
                if (user != null)
                    result.Add(studentId);
            }
            return result;
        }

        public async Task<List<int>?> checkValidStudentIdsAlreadyExitOrNot(List<int> studentIds, int classId,string operationType)
        {
            var validStudentIds = new List<int>();

            foreach( var studentId in studentIds)
            {
                var result = await _appDBContext.Enrollments.FirstOrDefaultAsync(s => s.ClassId == classId && s.StudentId  == studentId);
                if ((operationType == "ADD" && result == null) || (operationType == "REMOVE" && result != null))
                    validStudentIds.Add(studentId);
            }

            return validStudentIds;
        }

        public async Task<bool> AddStudentsToClass(List<int> validStudentIds,int classId)
        {
            var enrollments = validStudentIds.Distinct().Select( studentId => new Enrollment
            {
                ClassId = classId,
                StudentId = studentId
            });
            
            await _appDBContext.Enrollments.AddRangeAsync(enrollments);
            await _appDBContext.SaveChangesAsync();

            return true;
        }

        // check for duplicates in both addstudents and removestudents
        // also check whether student class pair already exit in case of add and exit in case of delete 
        // Optimize these loops in single query 
        public async Task<bool> RemoveStudentFromClass(List<int> validStudentIds,int classId)
        {
            var enrollments = _appDBContext.Enrollments.Where(e => e.ClassId == classId && validStudentIds.Contains(e.StudentId));
            if (enrollments == null)
                return false;

            _appDBContext.Enrollments.RemoveRange(entities: enrollments);
            await _appDBContext.SaveChangesAsync();
            return true;
        }

    }
}
