using Attendance_Manager.API.Data;
using Microsoft.AspNetCore.Identity;

namespace Attendance_Manager.API.JwtAuthentication
{
    public static class SeedData
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,AppDBContext dBContext)
        {
            string[] roles = { "Admin", "Faculty", "Student" };
            
            foreach (var role in roles)
            {
                if (! await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Default Admin User
            var adminEmail = "admin@college.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            
            if (admin == null)
            {
                var identityAdminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Default Admin"
                };
                var result = await userManager.CreateAsync(identityAdminUser, "Admin@123");
                
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(identityAdminUser, "Admin");

                var customAdminUser = new Attendance_Manager.API.Data.User
                {
                    UserName = identityAdminUser.FullName,
                    Email = identityAdminUser.Email,
                    Password = identityAdminUser.PasswordHash,
                    Role = "Admin",
                    IdentityUserID = identityAdminUser.Id
                };

                dBContext.Add(customAdminUser);
                dBContext.SaveChanges();
            }
        }
    }
}
