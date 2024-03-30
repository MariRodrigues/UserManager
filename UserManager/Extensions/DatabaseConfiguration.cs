using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.Model;

namespace UserManager.Extensions
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this IServiceCollection services, ConfigurationManager configuration)
        {
                services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            using var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            // Verifica se o banco de dados existe
            if (context.Database.GetPendingMigrations().Any())
            {
                try
                {
                    Console.WriteLine("Trying to create and migrate database");
                    context.Database.Migrate();
                }
                catch (SqlException exception) when (exception.Number == 1801)
                {
                    Console.WriteLine("Database already exists.");
                }

                context.SaveChanges();
            }

            Task.Run(() => CreateRoles(roleManager)).Wait();

            if (userManager.Users.Any())
            {
                return;
            }

            Task.Run(() => CreateUsers(userManager, context)).Wait();
        }

        private static async Task CreateRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole<int> adminRole = new IdentityRole<int>("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                IdentityRole<int> userRole = new IdentityRole<int>("User");
                await roleManager.CreateAsync(userRole);
            }
        }

        private static async Task CreateUsers(UserManager<CustomUser> userManager, AppDbContext context)
        {
            CustomUser admin = new()
            {
                Name = "Usuario Admin",
                UserName = "admin",
                Email = "admin@hotmail.com",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@HOTMAIL.COM",
                Status = true,
                CreatedOn = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            CustomUser user = new()
            {
                Name = "Usuario comum",
                UserName = "user",
                Email = "user@hotmail.com",
                NormalizedUserName = "USER",
                NormalizedEmail = "USER@HOTMAIL.COM",
                Status = true,
                CreatedOn = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<CustomUser> passwordHasher = new();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Pas$word!2");
            user.PasswordHash = passwordHasher.HashPassword(user, "Pas$word!2");

            context.Users.AddRange(admin, user);

            context.SaveChanges();

            await userManager.AddToRoleAsync(admin, "Admin");
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}
