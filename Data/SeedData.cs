using Microsoft.AspNetCore.Identity;
using Wheels_Away.Models;
using Wheels_Away.Data;
using Microsoft.Extensions.DependencyInjection;  // Make sure to include this namespace

public class SeedData
{
    public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
    {
        using (var serviceScope = serviceProvider.CreateScope())
        {
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            var adminUserEmail = "admin@test.com";
            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser()
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    EmailConfirmed = false,
                };

                // Adjusted password for complexity
                await userManager.CreateAsync(newAdminUser, "Admin123!");

                // Log or print the user information
                Console.WriteLine($"User Id: {newAdminUser.Id}, Email: {newAdminUser.Email}");

                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }

            var appUserEmail = "user@test.com";
            var appUser = await userManager.FindByEmailAsync(appUserEmail);

            if (appUser == null)
            {
                var newAppUser = new ApplicationUser()
                {
                    UserName = "user@test.com",
                    Email = "user@test.com",
                    EmailConfirmed = false,
                };

                // Adjusted password for complexity
                await userManager.CreateAsync(newAppUser, "User123!");

                // Log or print the user information
                Console.WriteLine($"User Id: {newAppUser.Id}, Email: {newAppUser.Email}");

                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}
