using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace ShopMVC
{
    public static class Seeder
    {
        public enum Roles { 
            User,
            Admin
        }

        //ijection dependency in class inizializator
        public static async Task SeedRoles(IServiceProvider app) {
            var roleManager = app.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in Enum.GetNames(typeof(Roles))) {
                if (!await roleManager.RoleExistsAsync(role)) {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }

        public static async Task SeedAdmin(IServiceProvider app) {
            var userManager = app.GetRequiredService<UserManager<User>>();
            const string USERNAME = "admin@admin.com";
            const string PASSWORD = "Admin_1";
            var existingUser = userManager.FindByEmailAsync(USERNAME).Result;
            if (existingUser == null) {
                var user = new User
                {
                    UserName = USERNAME,
                    Email=USERNAME,
                    EmailConfirmed=true
                    
                };
                var result=userManager.CreateAsync(user,PASSWORD).Result;
                if (result.Succeeded) {
                    userManager.AddToRoleAsync(user,"Admin").Wait();
                }
            }
          
        }
    }
}
