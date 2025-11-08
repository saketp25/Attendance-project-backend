using Microsoft.AspNetCore.Identity;

namespace Attendance_Manager.API.JwtAuthentication
{
    public static class SeedData
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
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
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Default Admin"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
