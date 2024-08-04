using Bogus;
using EBCustomerTask.Core.Entities;
using EBCustomerTask.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

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

            // Uygulama ayağa kalkarken, Rol ekler
            foreach (Role role in Enum.GetValues(typeof(Role)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    var identityRole = new IdentityRole(role.ToString());
                    await roleManager.CreateAsync(identityRole);
                }
            }

            // Uygulama ayağa kalkarken, User ve Admin ekler
            if (!dbContext.Users.Any())
            {
                var admin1 = new AppUser() { UserName = "admin1@gmail.com", Email = "admin1@gmail.com" };
                var admin2 = new AppUser() { UserName = "admin2@gmail.com", Email = "admin2@gmail.com" };
                var user1 = new AppUser() { UserName = "user1@gmail.com", Email = "user1@gmail.com" };
                var user2 = new AppUser() { UserName = "user2@gmail.com", Email = "user2@gmail.com" };

                await userManager.CreateAsync(admin1, "Password12*");
                await userManager.CreateAsync(admin2, "Password12*");
                await userManager.CreateAsync(user1, "Password12*");
                await userManager.CreateAsync(user2, "Password12*");

                await userManager.AddToRoleAsync(admin1, Role.Admin.ToString());
                await userManager.AddToRoleAsync(admin2, Role.Admin.ToString());
                await userManager.AddToRoleAsync(user1, Role.User.ToString());
                await userManager.AddToRoleAsync(user2, Role.User.ToString());
            }

            // SQLServer'da Customers'da kayıt yoksa, fake Customer ekler
            if (!dbContext.Customers.Any())
            {
                var faker = new Faker<Customer>()
                    .RuleFor(c => c.Id, f => f.Random.Guid().ToString())
                    .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                    .RuleFor(c => c.LastName, f => f.Name.LastName())
                    .RuleFor(c => c.Email, f => f.Internet.Email())
                    .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("##########"))
                    .RuleFor(c => c.BirthDate, f => f.Date.Past(30))
                    .RuleFor(c => c.PhotoUrl, f => f.Internet.Avatar());

                var customers = faker.Generate(10);

                await dbContext.Customers.AddRangeAsync(customers);
                await dbContext.SaveChangesAsync();
            }

            var mongoClient = scope.ServiceProvider.GetRequiredService<MongoClient>();
            var mongoDatabase = mongoClient.GetDatabase("EBCustomerTaskDb");
            var customerCollection = mongoDatabase.GetCollection<Customer>("Customers");

            // MongoDb'de Customers koleksiyonun'da kayıt yoksa fake kayıt ekler
            if (!customerCollection.AsQueryable().Any())
            {
				var faker = new Faker<Customer>()
					.RuleFor(c => c.Id, f => ObjectId.GenerateNewId().ToString())
					.RuleFor(c => c.FirstName, f => f.Name.FirstName())
					.RuleFor(c => c.LastName, f => f.Name.LastName())
					.RuleFor(c => c.Email, f => f.Internet.Email())
					.RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("##########"))
					.RuleFor(c => c.BirthDate, f => f.Date.Past(30))
					.RuleFor(c => c.PhotoUrl, f => f.Internet.Avatar());

				var customers = faker.Generate(10);

				await customerCollection.InsertManyAsync(customers);
			}
        }
    }
}
