using CashRegister.Web.DataAccess;
using CashRegister.Web.Models.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashRegister.Web.App_Start
{
    public static class Startup_DbContext
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
                var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().CreateAdminAccount(userManager, roleManager);
            }
        }
    }
}
