using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TabulationSystem.Areas.Identity.Data;
using TabulationSystem.Data;
namespace TabulationSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection")
                ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure authentication cookie settings
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login"; // Redirect to Login when unauthorized
                options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Redirect when access is denied
                options.LogoutPath = "/Identity/Account/Logout"; // Logout path
                options.SlidingExpiration = true; // Extend session on activity
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Set session timeout to 60 minutes
            });

            // Disable email confirmation requirement
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false; // Allow login without email confirmation
            });

            builder.Services.AddRazorPages();//required for the identity UI

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); // Ensure authentication is enabled
            app.UseAuthorization();
            app.MapRazorPages(); // important for identity UI pages

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dashboard}/{action=Index}/{id?}")
                .WithStaticAssets();

            // SEED ROLES & ADMIN USER
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await SeedRolesAndAdmin(services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding roles: {ex.Message}");
                }
            }

            app.Run();
        }

        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Define roles
                string[] roleNames = { "Admin", "Judge", "Manager" };

                foreach (var roleName in roleNames)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                        Console.WriteLine($"Role '{roleName}' created.");
                    }
                }

                // Create Admin User if not exists
                string adminEmail = "admin@tabulate.com";
                string adminPassword = "Admin@123";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        FirstName = "System",
                        LastName = "Admin",
                        EmailConfirmed = true,
                        DateTimeCreated = DateTime.Now,
                        DateTimeUpdated = DateTime.Now
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        Console.WriteLine("Admin user created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create Admin user: " + string.Join(", ", result.Errors));
                    }
                }

                // Assign Admin Role
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Admin user assigned to Admin role.");
                }
            }
        }
    }
}
