using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EBCustomerTask.Infrastructure.Data
{
    public static class DataSeed
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await dbContext.Database.MigrateAsync();

            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    var identityRole = new IdentityRole(role.ToString());
                    await roleManager.CreateAsync(identityRole);
                }
            }

            if (!dbContext.Users.Any())
            {
                var admin1 = new AppUser() { UserName = "admin1", Email = "admin1@gmail.com" };
                var admin2 = new AppUser() { UserName = "admin2", Email = "admin2@gmail.com" };
                var user1 = new AppUser() { UserName = "user1", Email = "user1@gmail.com" };
                var user2 = new AppUser() { UserName = "user2", Email = "user2@gmail.com" };

                await userManager.CreateAsync(admin1, "Password12*");
                await userManager.CreateAsync(admin2, "Password12*");
                await userManager.CreateAsync(user1, "Password12*");
                await userManager.CreateAsync(user2, "Password12*");

                await userManager.AddToRoleAsync(admin1, Role.Admin.ToString());
                await userManager.AddToRoleAsync(admin2, Role.Admin.ToString());
                await userManager.AddToRoleAsync(user1, Role.User.ToString());
                await userManager.AddToRoleAsync(user2, Role.User.ToString());
            }

        }
    }
}
