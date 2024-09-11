using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Utility;

public class SeedData
{
    public static async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(SD.Role_Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
        }

        if (!await roleManager.RoleExistsAsync(SD.Role_User_Customer))
        {
            await roleManager.CreateAsync(new IdentityRole(SD.Role_User_Customer));
        }

        var adminEmail = "admin@domain.com";
        var adminPassword = "Admin@123"; 
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
            }
        }
    }
}
